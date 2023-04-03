using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarButtonAnimationManager : MonoBehaviour
{
    public Animator animator;
    public bool test = false;

    public void DelayAnimation()
    {
        Debug.Log("Delay started");
        int z = Random.Range(0, 5);
        StartCoroutine(DelayBotAction(z));
    }

    private void Update()
    {
        //Debugging:
        //if (test)
        //{
        //    StartCoroutine(RunButtonPushinAnim());
        //    print("hi");
        //    test = false;
        //}
    }

    IEnumerator DelayBotAction(int delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(RunButtonPushinAnim());
    }


    IEnumerator RunButtonPushinAnim()
    {
        animator.SetBool("ButtonPush", true);
        yield return new WaitForSeconds(1.8f);
        animator.SetBool("ButtonPush", false);
    }
}
