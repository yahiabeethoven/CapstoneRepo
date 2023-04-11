using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestScore : MonoBehaviour
{
    public TMPro.TMP_Text subjectScore;
    public TMPro.TMP_Text computerScore;
    public TMPro.TMP_Text subjectTotal;
    public TMPro.TMP_Text computerTotal;
    public TMPro.TMP_Text totalLabel;

    public GameObject[] rows;
    private GameObject rowObject;

    private Color lightGreen = new Color(0.56f, 0.93f, 0.56f);
    private Color lightBlue = new Color(0.678f, 0.847f, 0.902f);
    Color lightGrey = Color.Lerp(Color.white, Color.grey, 0.5f);

    public void Start()
    {
        rows = new GameObject[11];
        rows[0] = GameObject.Find("Row1");
        rows[0].SetActive(false);
        rows[1] = GameObject.Find("Row2");
        rows[1].SetActive(false);
        rows[2] = GameObject.Find("Row3");
        rows[2].SetActive(false);
        rows[3] = GameObject.Find("Row4");
        rows[3].SetActive(false);
        rows[4] = GameObject.Find("Row5");
        rows[4].SetActive(false);
        rows[5] = GameObject.Find("Row6");
        rows[5].SetActive(false);
        rows[6] = GameObject.Find("Row7");
        rows[6].SetActive(false);
        rows[7] = GameObject.Find("Row8");
        rows[7].SetActive(false);
        rows[8] = GameObject.Find("Row9");
        rows[8].SetActive(false);
        rows[9] = GameObject.Find("Row10");
        rows[9].SetActive(false);
        rows[10] = GameObject.Find("RowTotal");
        rows[10].SetActive(false);
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
        rows[10].SetActive(true);
        for (int i = 0; i < rows.Length - 1; i++)
        {
            if (i < phase - 1) // Previous rounds
            {
                rows[i].SetActive(true); // Show the row
                TMP_Text roundLabel = rows[i].transform.Find("Round" + (i + 1) + "Label").GetComponentInChildren<TMP_Text>();
                TMP_Text subjectOldScore = rows[i].transform.Find("SubjectScore" + (i + 1)).GetComponentInChildren<TMP_Text>();
                TMP_Text opponentOldScore = rows[i].transform.Find("OpponentScore" + (i + 1)).GetComponentInChildren<TMP_Text>();

                subjectOldScore.color = Color.white;
                opponentOldScore.color = Color.white;
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

            subjectScore.color = lightGreen;
            computerScore.color = lightGreen;
        }

        totalLabel = rows[10].transform.Find("RoundTotalLabel").GetComponentInChildren<TMP_Text>();
        subjectTotal = rows[10].transform.Find("SubjectScoreTotal").GetComponentInChildren<TMP_Text>();
        computerTotal = rows[10].transform.Find("OpponentScoreTotal").GetComponentInChildren<TMP_Text>();

        subjectTotal.text = (float.Parse(subjectTotal.text) + subjectChange).ToString();
        computerTotal.text = (float.Parse(computerTotal.text) + computerChange).ToString();

        totalLabel.color = Color.grey;
        subjectTotal.color = lightGrey;
        computerTotal.color = lightGrey;
    }
}
