using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarButtonAnimationManager : MonoBehaviour
{
    public Animator animator;
    public bool test = false;
    private int buttonIndex;
    public GameObject ScoreTable;

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
        if (buttonIndex == 0)
        {
            ScoreTable.GetComponent<TestScore>().ChangeScore(+10f, +0f);
            //GameObject.Find("Score Table").GetComponent<TestScore>().ChangeScore(+10f, +0f);
        }
        else
        {
            ScoreTable.GetComponent<TestScore>().ChangeScore(+0f, +10f);
            //GameObject.Find("Score Table").GetComponent<TestScore>().ChangeScore(+0f, +10f);
        }
    }
}
