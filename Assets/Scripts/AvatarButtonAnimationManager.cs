using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Globalization;
using System;
using System.Linq;
using TMPro;
using UnityEngine.Android;


public class AvatarButtonAnimationManager : MonoBehaviour
{
    public Animator animator;
    public GameObject ScoreTable;
    private Button thisButton;

    private int buttonIndex;
    private int prevButtonIndex = 0;           // this is the simulated player's way of keeping track of the subject's last move to copy it (tit for tat)
    private readonly int cooperateIndex = 0;
    private readonly int defectIndex = 1;

    private float mutualCooperate = +3f;
    private float singleDefect = +5f;
    private float singleCooperate = -1f;
    private float mutualDefect = 0f;

    private int bothCoop = 0;
    private int firstDefect = 1;
    private int secondDefect = 2;
    private int bothDefect = 3;
    private int currentOutcome = -1;

    private int currentPhase = 1;
    private int currentRound = 1;

    private string csvFilePath;
    private string updateMsg;

    public TMPro.TMP_Text testMsg;

    private StringBuilder csvContent = new StringBuilder();
    public AvatarRandomizationManager avatarRandomizationManager;
    

    public class MyData
    {
        public string SubjectId { get; set; }
        public string OpponentId { get; set; }
        public int Phase { get; set; }
        public int RoundNumber { get; set; }
        public int SubjectChoice { get; set; }
        public int OpponentChoice { get; set; }
    }

    public MyData myData;

    public void WriteToCsvFile(string filePath, MyData data)
    {
        // Check if the file already exists.
        bool fileExists = File.Exists(filePath);

        // Open the output file for appending.
        using (var writer = new StreamWriter(filePath, true))
        {
            // Write the header only if the file doesn't exist yet.
            if (!fileExists)
            {
                var header = string.Join(",", typeof(MyData).GetProperties().Select(p => p.Name));
                writer.WriteLine(header);
            }

            // Write the data to the CSV file.
            var values = string.Join(",", typeof(MyData).GetProperties().Select(p => p.GetValue(data)));
            writer.WriteLine(values);

        }
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("ButtonPush", false);

        avatarRandomizationManager = AvatarRandomizationManager.Instance;

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            csvFilePath = Application.persistentDataPath + "/_test.csv";
        }

        print(csvFilePath);
    }

    private void Update()
    {

        if (GameObject.FindGameObjectWithTag("Transporter").GetComponent<TransporterController>().destination == "Area 1" && !ScoreTable)
        {
            ScoreTable = GameObject.Find("ScoreTable");
            testMsg = GameObject.Find("Instructions").GetComponent<TMP_Text>();
            testMsg.text = "current path: " + csvFilePath;
        }  
    }

    public void DelayAnimation(Button button,  int bIndex)
    {
        thisButton = button;
        buttonIndex = bIndex;
        Debug.Log("Delay started");
        int z = UnityEngine.Random.Range(0, 3);
        if (animator.GetBool("ButtonPush") != true)
        {
            StartCoroutine(DelayBotAction(z));
            myData = new MyData
            {
                SubjectId = avatarRandomizationManager.subjectAvatarRace,
                OpponentId = avatarRandomizationManager.opponentAvatarRace,
                Phase = currentPhase,
                RoundNumber = currentRound,
                SubjectChoice = buttonIndex,
                OpponentChoice = prevButtonIndex
            };

            WriteToCsvFile(csvFilePath, myData);
        }
    }

    IEnumerator DelayBotAction(int delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(RunButtonPushingAnimation());
    }


    IEnumerator RunButtonPushingAnimation()
    {
        animator.SetBool("ButtonPush", true);
        yield return new WaitForSeconds(1.8f);

        if (prevButtonIndex == cooperateIndex) // if opponent pushed button 0 last time, push button 0 this time
        {
            if (buttonIndex == cooperateIndex)
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, mutualCooperate, mutualCooperate);
                currentOutcome = bothCoop;
            }
            else
            {
                prevButtonIndex = defectIndex;
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, singleDefect, singleCooperate);
                currentOutcome = firstDefect;
            }
        }
        else // if opponent pushed button 1 last time, push button 1 this time
        {
            if (buttonIndex == cooperateIndex)
            {
                prevButtonIndex = cooperateIndex;
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, singleCooperate, singleDefect);
                currentOutcome = secondDefect;
            }
            else
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, mutualDefect, mutualDefect);
                currentOutcome = bothDefect;
            }
            
        }
        
        

        if (currentOutcome == bothCoop)
        {
            //updateMsg = "Mini-Round: "+currentRound+"\nYou: +" + mutualCooperate.ToString() + "\nOpponent: +" + mutualCooperate.ToString() + "\n\nOutcome: you both cooperated";
            updateMsg = myData.SubjectId.ToString()+","+ myData.OpponentId.ToString() + "," + myData.Phase.ToString() + "," + myData.RoundNumber.ToString() + "," + myData.SubjectChoice.ToString() + "," + myData.OpponentChoice.ToString();
        }
        else if (currentOutcome == firstDefect)
        {
            updateMsg = "Mini-Round: " + currentRound + "\nYou: +" + singleDefect.ToString() + "\nOpponent: " + singleCooperate.ToString() + "\n\nOutcome: you defected, the opponent cooperated";
        }
        else if (currentOutcome == secondDefect)
        {
            updateMsg = "Mini-Round: " + currentRound + "\nYou: " + singleCooperate.ToString() + "\nOpponent: +" + singleDefect.ToString() + "\n\nOutcome: you cooperated, the opponent defected";
        }
        else if (currentOutcome == bothDefect)
        {
            updateMsg = "Mini-Round: " + currentRound + "\nYou: +" + mutualDefect.ToString() + "\nOpponent: +" + mutualDefect.ToString() + "\n\nOutcome: you both defected";
        }

        if (currentRound == 10)
        {
            updateMsg += "\n\nRound "+currentPhase.ToString()+" completed!";
        }
        thisButton.GetComponent<OnButtonClick>().ShowPopup(updateMsg);
        animator.SetBool("ButtonPush", false);

        if (currentRound == 10 && currentPhase == 5)
        {
            Debug.Log("All Rounds are done!");
            updateMsg = "Thank you very much for participating in this experiment!\n\nPlease take off the headset and notify your lab assistant that you are done!";
            thisButton.GetComponent<OnButtonClick>().ShowPopup(updateMsg);
            //ScoreTable.SetActive(false);
        }
        else if (currentRound == 10)
        {
            currentPhase++;
            currentRound = 1;
        }
        else
        {
            currentRound++;
        }
    }
}
