using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestScore : MonoBehaviour
{
    public TMPro.TMP_Text subjectScore;
    public TMPro.TMP_Text computerScore;
    public GameObject[] rows;
    private GameObject rowObject;

    public void Start()
    {
        rows = new GameObject[11];
        rows[0] = GameObject.Find("Row1");
        rows[1] = GameObject.Find("Row2");
        rows[2] = GameObject.Find("Row3");
        rows[3] = GameObject.Find("Row4");
        rows[4] = GameObject.Find("Row5");
        rows[5] = GameObject.Find("Row6");
        rows[6] = GameObject.Find("Row7");
        rows[7] = GameObject.Find("Row8");
        rows[8] = GameObject.Find("Row9");
        rows[9] = GameObject.Find("Row10");
        rows[10] = GameObject.Find("RowTotal");
    }

    public GameObject FindRow(int rowNumber)
    {
        if (rowNumber >= 1 && rowNumber <= 11)
        {
            return rows[rowNumber - 1]; // Return the row GameObject
        }
        else
        {
            Debug.LogError("Invalid row number: " + rowNumber);
            return null; // Return null if an invalid row number is passed
        }
    }

    private void Update()
    {
        //ChangeScore(+10f, +10f);
    }
    public void ChangeScore(int phase, float subjectChange, float computerChange)
    {
        //rowObject = FindRow(phase);
        //if (rowObject != null)
        //{
        //    subjectScore = rowObject.transform.Find("SubjectScore" + phase.ToString()).GetComponentInChildren<TMP_Text>();
        //    computerScore = rowObject.transform.Find("OpponentScore" + phase.ToString()).GetComponentInChildren<TMP_Text>();

        //    subjectScore.text = (float.Parse(subjectScore.text) + subjectChange).ToString();
        //    computerScore.text = (float.Parse(computerScore.text) + computerChange).ToString();
        //}
        // Loop through all rows

        for (int i = 0; i < rows.Length; i++)
        {
            if (i < phase - 1) // Previous rounds
            {
                rows[i].SetActive(true); // Show the row
                TMP_Text roundLabel = rows[i].transform.Find("Round" + (i + 1) + "Label").GetComponentInChildren<TMP_Text>();
                roundLabel.color = Color.white; // Set the color to white
            }
            else if (i == phase - 1) // Current round
            {
                rows[i].SetActive(true); // Show the row
                TMP_Text roundLabel = rows[i].transform.Find("Round" + phase + "Label").GetComponentInChildren<TMP_Text>();
                roundLabel.color = Color.green; // Set the color to green
            }
            else // Future rounds
            {
                rows[i].SetActive(false); // Hide the row
            }
        }

        rowObject = FindRow(phase);
        if (rowObject != null)
        {
            subjectScore = rowObject.transform.Find("SubjectScore" + phase.ToString()).GetComponentInChildren<TMP_Text>();
            computerScore = rowObject.transform.Find("OpponentScore" + phase.ToString()).GetComponentInChildren<TMP_Text>();

            subjectScore.text = (float.Parse(subjectScore.text) + subjectChange).ToString();
            computerScore.text = (float.Parse(computerScore.text) + computerChange).ToString();

            subjectScore.color = Color.green;
            computerScore.color = Color.green;
        }
    }
}
