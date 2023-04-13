using System.Collections;
using System.Collections.Generic;
using System.Text; // add this namespace for StringBuilder
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ExportCSV : MonoBehaviour
{
    private StringBuilder csvContent = new StringBuilder(); // create a new StringBuilder object

    public void ExportDataToCSV(string fileName)
    {
        // Add header row to CSV content
        csvContent.AppendLine("Round Number,Subject Choice,Computer Choice");

        // Loop through data and add rows to CSV content
        for (int i = 1; i <= 10; i++)
        {
            // Replace these variables with your actual data
            int roundNumber = i;
            int subjectChoice = Random.Range(0, 2);
            int computerChoice = Random.Range(0, 2);

            // Add row to CSV content
            csvContent.AppendLine(roundNumber + "," + subjectChoice + "," + computerChoice);
        }

        // Write CSV content to file
        File.WriteAllText(fileName, csvContent.ToString());

        Debug.Log("Data exported to CSV file: " + fileName);
    }
}
