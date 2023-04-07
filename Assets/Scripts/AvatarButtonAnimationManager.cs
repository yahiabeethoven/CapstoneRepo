using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarButtonAnimationManager : MonoBehaviour
{
    public Animator animator;
    public bool test = false;
    private int buttonIndex;
    public GameObject ScoreTable;
    private int prevButtonIndex = -1;

    public void DelayAnimation(int bIndex)
    {
        buttonIndex = bIndex;
        Debug.Log("Delay started");
        int z = Random.Range(0, 5);
        StartCoroutine(DelayBotAction(z));
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
        animator.SetBool("ButtonPush", false);

        if (prevButtonIndex == -1) // first move, push button 0
        {
            if (buttonIndex == 0)
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(-2f, -2f);
            }
            else
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(+0f, -10f);
            }
            prevButtonIndex = 0;
        }
        else if (prevButtonIndex == 0) // if opponent pushed button 0 last time, push button 0 this time
        {
            if (buttonIndex == 0)
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(-2f, -2f);
            }
            else
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(+0f, -10f);
                prevButtonIndex = 1;
            }
        }
        else // if opponent pushed button 1 last time, push button 1 this time
        {
            if (buttonIndex == 0)
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(-10f, 0f);
                prevButtonIndex = 0;
            }
            else
            {
                ScoreTable.GetComponent<TestScore>().ChangeScore(-5f, -5f);
            }
            
        }
        //if (buttonIndex == 0)
        //{
        //    ScoreTable.GetComponent<TestScore>().ChangeScore(+10f, +0f);
        //}
        //else
        //{
        //    ScoreTable.GetComponent<TestScore>().ChangeScore(+0f, +10f);
        //}
    }
}
