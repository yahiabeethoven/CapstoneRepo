using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AvatarButtonAnimationManager : MonoBehaviour
{
    public Animator animator;
    public GameObject ScoreTable;
    private Button thisButton;
    private Button diffButton;

    private int buttonIndex;
    private int prevButtonIndex = -1;           // this is the simulated player's way of keeping track of the subject's last move to copy it (tit for tat)
    private readonly int firstMove = -1;
    private readonly int cooperateIndex = 0;
    private readonly int defectIndex = 1;

    private float mutualCooperate = -2f;
    private float singleDefect = +0f;
    private float singleCooperate = -10f;
    private float mutualDefect = -5f;

    private int bothCoop = 0;
    private int firstDefect = 1;
    private int secondDefect = 2;
    private int bothDefect = 3;
    private int currentOutcome = -1;

    private int currentPhase = 1;
    private int currentRound = 1;

    //private string csvFilePath = "Assets/scores.csv";
    private string csvFilePath;
    private string updateMsg;
    private StringBuilder csvContent = new StringBuilder();


    public Text t;
    public void Start()
    {
        animator = GetComponent<Animator>();
        ScoreTable = GameObject.Find("ScoreTable");

        csvFilePath = Application.dataPath + "/Data/test.csv";
        print(Application.dataPath);
        if (t != null)
            t.text = Application.dataPath;
         //csvFilePath = "Assets/scores.csv";
        // create or open CSV file

        StreamWriter writer = new StreamWriter(csvFilePath, true); // set the second parameter to true to append to the file
        csvContent.Append("Round Number,Subject Choice,Computer Choice\n"); // add column headers
        writer.Write(csvContent); // write the headers to the CSV file
        writer.Close(); // close the file
        System.IO.File.WriteAllText(Application.dataPath + "/DataForExport.txt", csvContent.ToString());

        /*
        "Round Number,Subject Choice,Computer Choice\n"
        "2, cooperate, not cooperate"





         */
    }

    public void UpdateCSV(int roundNumber, int subjectChoice, int computerChoice)
    {
        csvContent.Clear();
        // add the round number, subject choice, and computer choice to the CSV content
        csvContent.Append(roundNumber.ToString() + "," + subjectChoice.ToString() + "," + computerChoice.ToString() + "\n");

        // write the updated CSV content to the file
        StreamWriter writer = new StreamWriter(csvFilePath, true); // set the second parameter to false to overwrite the file
        writer.Write(csvContent);
        writer.Close();
    }

    public void DelayAnimation(Button button, Button other, int bIndex)
    {
        diffButton = other;
        thisButton = button;
        buttonIndex = bIndex;
        Debug.Log("Delay started");
        int z = Random.Range(0, 3);
        if (animator.GetBool("ButtonPush") != true)
        {
            StartCoroutine(DelayBotAction(z));
            
            if (currentRound == 10)
            {
                if (currentPhase == 10)
                {
                    Debug.Log("All Rounds are done!");
                }
                else
                {
                    currentPhase++;
                    currentRound = 1;
                }
            }
            else
            {
                currentRound++;
            }
            UpdateCSV(currentRound, buttonIndex, prevButtonIndex);
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
        
        if (prevButtonIndex == firstMove) // first move, push button 0
        {
            if (buttonIndex == cooperateIndex)
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, mutualCooperate, mutualCooperate);
                prevButtonIndex = cooperateIndex;
                currentOutcome = bothCoop;
            }
            else
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, singleDefect, singleCooperate);
                prevButtonIndex = defectIndex;
                currentOutcome = firstDefect;
            }    
        }
        else if (prevButtonIndex == cooperateIndex) // if opponent pushed button 0 last time, push button 0 this time
        {
            if (buttonIndex == cooperateIndex)
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, mutualCooperate, mutualCooperate);
                currentOutcome = bothCoop;
            }
            else
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, singleDefect, singleCooperate);
                prevButtonIndex = defectIndex;
                currentOutcome = firstDefect;
            }
        }
        else // if opponent pushed button 1 last time, push button 1 this time
        {
            if (buttonIndex == cooperateIndex)
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, singleCooperate, singleDefect);
                prevButtonIndex = cooperateIndex;
                currentOutcome = secondDefect;
            }
            else
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, mutualDefect, mutualDefect);
                currentOutcome = bothDefect;
            }
            
        }
        animator.SetBool("ButtonPush", false);
        thisButton.interactable = true;
        diffButton.interactable = true;

        if (currentOutcome == bothCoop)
        {
            updateMsg = "You: " + mutualCooperate.ToString() + "\nOpponent: " + mutualCooperate.ToString() + "\n\nOutcome: you both cooperated";
        }
        else if (currentOutcome == firstDefect)
        {
            updateMsg = "You: +" + singleDefect.ToString() + "\nOpponent: " + singleCooperate.ToString() + "\n\nOutcome: you defected, the opponent cooperated";
        }
        else if (currentOutcome == secondDefect)
        {
            updateMsg = "You: " + singleCooperate.ToString() + "\nOpponent: +" + singleDefect.ToString() + "\n\nOutcome: you cooperated, the opponent defected";
        }
        else if (currentOutcome == bothDefect)
        {
            updateMsg = "You: " + mutualDefect.ToString() + "\nOpponent: " + mutualDefect.ToString() + "\n\nOutcome: you both defected";
        }

        if (currentRound == 10)
        {
            updateMsg += "\n\nRound "+currentPhase.ToString()+" completed!";
        }
        thisButton.GetComponent<OnButtonClick>().ShowPopup(updateMsg);
        
    }
}
