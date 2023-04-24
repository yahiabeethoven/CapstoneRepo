using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
//using CsvHelper;
using System.IO;
//using CsvHelper.Configuration;
using System.Globalization;
using System;
using System.Linq;
using TMPro;
using UnityEngine.Android;

//using CsvHelper;
//using static UnityEditor.PlayerSettings;

public class AvatarButtonAnimationManager : MonoBehaviour
{
    public Animator animator;
    public GameObject ScoreTable;
    private Button thisButton;

    private int buttonIndex;
    private int prevButtonIndex = 0;           // this is the simulated player's way of keeping track of the subject's last move to copy it (tit for tat)
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

    public TMPro.TMP_Text testMsg;
    public TMPro.TMP_Text testMsg2;

    private StringBuilder csvContent = new StringBuilder();
    public Text t;
    public AvatarRandomizationManager avatarRandomizationManager;
    

    public class MyData
    {
        public int SubjectId { get; set; }
        public int OpponentId { get; set; }
        public int Phase { get; set; }
        public int RoundNumber { get; set; }
        public int SubjectChoice { get; set; }
        public int OpponentChoice { get; set; }
    }

    public MyData myData;


    //public void WriteToCsvFile(string filePath, MyData data)
    //{
    //    // Configure the CSV writer.
    //    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    //    {
    //        HasHeaderRecord = true, // Include a header row in the CSV file.
    //        Delimiter = ",", // Use a comma as the field delimiter.
    //    };

    //    // Open the output file.
    //    using (var writer = new StreamWriter(filePath))
    //    {
    //        // Create the CSV writer.
    //        using (var csv = new CsvWriter(writer, config))
    //        {
    //            // Write the data to the CSV file.
    //            csv.WriteRecord(data);
    //        }
    //    }
    //}
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



    //public void WriteToCsvFile(string filePath, MyData data)
    //{
    //    var exists = File.Exists(filePath);
    //    using (var writer = new StreamWriter(filePath, true))
    //    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //    {
    //        if (!exists)
    //        {
    //            csv.WriteHeader<MyData>();
    //            csv.NextRecord();
    //        }
    //        csv.WriteRecord(data);
    //        csv.NextRecord();
    //        try
    //        {
    //            writer.Flush();
    //        }
    //        catch (Exception ex)
    //        {
    //            // Handle the error here, e.g. log the error or retry writing.
    //            Console.WriteLine($"Error writing to CSV file: {ex.Message}");
    //        }
    //    }
    //}
    //public static void WriteToCsvFile(string filePath, MyData data)
    //{
    //    using (var streamWriter = new StreamWriter(filePath, true))
    //    using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
    //    {
    //        csv.Configuration.Delimiter = ",";
    //        csv.Configuration.HasHeaderRecord = !File.Exists(filePath);

    //        if (csv.Configuration.HasHeaderRecord)
    //        {
    //            csv.WriteHeader<MyData>();
    //            csv.NextRecord();
    //        }

    //        csv.WriteRecord(data);
    //        csv.NextRecord();
    //        streamWriter.Flush();
    //    }
    //}

    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("ButtonPush", false);

        avatarRandomizationManager = AvatarRandomizationManager.Instance;

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }

        //var externalStoragePath = "";

        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    // Get the path to the external storage directory
        //    AndroidJavaClass environmentClass = new AndroidJavaClass("android.os.Environment");
        //    externalStoragePath = environmentClass.CallStatic<AndroidJavaObject>("getExternalStorageDirectory").Call<string>("getAbsolutePath");
        //}

        //// Create a new directory for your app's files
        //var appDirectoryPath = Path.Combine(externalStoragePath, "MyAppDirectory");
        //Directory.CreateDirectory(appDirectoryPath);

        //// Write the data to a CSV file in the app directory
        //csvFilePath = Path.Combine(appDirectoryPath, "myData.csv");

        if (Application.platform == RuntimePlatform.Android)
        {
            // Get the path to the external storage directory
            string externalStoragePath = "/storage/self/primary";
            string folderName = "CapstoneFiles";
            string csvFileName = "test.csv";

            // Create the folder if it doesn't exist
            string folderPath = Path.Combine(externalStoragePath, folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Create the CSV file if it doesn't exist
            csvFilePath = Path.Combine(folderPath, csvFileName);
            if (!File.Exists(csvFilePath))
            {
                File.Create(csvFilePath).Close();
            }
        }

        //csvFilePath = Application.dataPath + "/Data/test.csv";
        //if (t != null)
        //    t.text = Application.dataPath;
        //csvFilePath = "Assets/scores.csv";
        print(csvFilePath);
        //// create or open CSV file

        //StreamWriter writer = new StreamWriter(csvFilePath, true); // set the second parameter to true to append to the file
        //csvContent.Append("Subject ID, Opponent ID, Phase Number, Round Number,Subject Choice,Opponent Choice\n"); // add column headers
        //writer.Write(csvContent); // write the headers to the CSV file
        //writer.Close(); // close the file
        //System.IO.File.WriteAllText(Application.dataPath + "/DataForExport.txt", csvContent.ToString());


        /*
        "Round Number,Subject Choice,Computer Choice\n"
        "2, cooperate, not cooperate"





         */
    }

    private void Update()
    {

        if (GameObject.FindGameObjectWithTag("Transporter").GetComponent<TransporterController>().destination == "Area 1" && !ScoreTable)
        {
            ScoreTable = GameObject.Find("ScoreTable");
            testMsg = GameObject.Find("Instructions").GetComponent<TMP_Text>();
            testMsg2 = GameObject.Find("OpponentDescription").GetComponent<TMP_Text>();
            testMsg.text = "current path: " + csvFilePath;
            if (myData != null)
            {
                testMsg2.text = myData.ToString();
            }
            
        }

        
    }

    public void UpdateCSV(int subject, int opponent, int phase, int roundNumber, int subjectChoice, int computerChoice)
    {
        //csvContent.Clear();
        //// add the round number, subject choice, and computer choice to the CSV content
        //csvContent.Append(subject.ToString() +","+ opponent.ToString() + "," + phase.ToString() + "," + roundNumber.ToString() + "," + subjectChoice.ToString() + "," + computerChoice.ToString() + "\n");

        //// write the updated CSV content to the file
        //StreamWriter writer = new StreamWriter(csvFilePath, true); // set the second parameter to false to overwrite the file
        //writer.Write(csvContent);
        //writer.Close();

        // add the round number, subject choice, and computer choice to the CSV content
        csvContent.Append(subject.ToString() + "," + opponent.ToString() + "," + phase.ToString() + "," + roundNumber.ToString() + "," + subjectChoice.ToString() + "," + computerChoice.ToString() + "\n");

        // write the updated CSV content to the file
        StreamWriter writer = new StreamWriter(csvFilePath, true); // set the second parameter to true to append to the file
        writer.Write(csvContent);
        writer.Close();
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
                SubjectId = avatarRandomizationManager.subjectAvatarIndex,
                OpponentId = avatarRandomizationManager.opponentAvatarIndex,
                Phase = currentPhase,
                RoundNumber = currentRound,
                SubjectChoice = buttonIndex,
                OpponentChoice = prevButtonIndex
            };
            WriteToCsvFile(csvFilePath, myData);
            //UpdateCSV(avatarRandomizationManager.subjectAvatarIndex, avatarRandomizationManager.opponentAvatarIndex, currentPhase, currentRound, buttonIndex, prevButtonIndex);
            //UpdateCSV(currentRound, buttonIndex, prevButtonIndex);
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
            //updateMsg = "Mini-Round: "+currentRound+"\nYou: " + mutualCooperate.ToString() + "\nOpponent: " + mutualCooperate.ToString() + "\n\nOutcome: you both cooperated";
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
            updateMsg = "Mini-Round: " + currentRound + "\nYou: " + mutualDefect.ToString() + "\nOpponent: " + mutualDefect.ToString() + "\n\nOutcome: you both defected";
        }

        if (currentRound == 10)
        {
            updateMsg += "\n\nRound "+currentPhase.ToString()+" completed!";
        }
        thisButton.GetComponent<OnButtonClick>().ShowPopup(updateMsg);
        animator.SetBool("ButtonPush", false);

        //UpdateCSV(avatarRandomizationManager.subjectAvatarIndex, avatarRandomizationManager.opponentAvatarIndex, currentPhase, currentRound, buttonIndex, prevButtonIndex);

        if (currentRound == 10 && currentPhase == 10)
        {
            Debug.Log("All Rounds are done!");
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
