using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarButtonAnimationManager : MonoBehaviour
{
    public Animator animator;
    public GameObject ScoreTable;
    private Button thisButton;
    private Button diffButton;

    private int buttonIndex;
    private int prevButtonIndex = -1;           // this is the simulated player's way of keeping track of the subject's last move to copy it (tit for tat)
    private int firstMove = -1;
    private int cooperateIndex = 0;
    private int defectIndex = 1;

    private float mutualCooperate = -2f;
    private float singleDefect = +0f;
    private float singleCooperate = -10f;
    private float mutualDefect = -5f;

    private int currentPhase = 1;
    private int currentRound = 1;

    public void DelayAnimation(Button button, Button other, int bIndex)
    {
        diffButton = other;
        thisButton = button;
        buttonIndex = bIndex;
        Debug.Log("Delay started");
        int z = Random.Range(0, 5);
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
            }
            else
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, singleDefect, singleCooperate);
                prevButtonIndex = defectIndex;
            }    
        }
        else if (prevButtonIndex == cooperateIndex) // if opponent pushed button 0 last time, push button 0 this time
        {
            if (buttonIndex == cooperateIndex)
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, mutualCooperate, mutualCooperate);
            }
            else
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, singleDefect, singleCooperate);
                prevButtonIndex = defectIndex;
            }
        }
        else // if opponent pushed button 1 last time, push button 1 this time
        {
            if (buttonIndex == cooperateIndex)
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, singleCooperate, singleDefect);
                prevButtonIndex = cooperateIndex;
            }
            else
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(currentPhase, mutualDefect, mutualDefect);
            }
            
        }
        animator.SetBool("ButtonPush", false);
        thisButton.interactable = true;
        diffButton.interactable = true;
        if (thisButton.tag == "CooperateButton")
        {
            thisButton.GetComponent<OnButtonClick>().ShowPopup("Cooperate button pressed");
        }
        else
        {
            thisButton.GetComponent<OnButtonClick>().ShowPopup("Defect button pressed");
        }
        
    }
}
