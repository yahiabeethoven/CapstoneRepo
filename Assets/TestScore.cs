using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScore : MonoBehaviour
{
    public TMPro.TMP_Text subjectScore;
    public TMPro.TMP_Text computerScore;    


    private void Update()
    {
        //ChangeScore(+10f, +10f);
    }
    public void ChangeScore(float subjectChange, float computerChange)
    {
        subjectScore.text = (float.Parse(subjectScore.text) + subjectChange).ToString();
        computerScore.text = (float.Parse(computerScore.text) + computerChange).ToString();
                
    }
}
