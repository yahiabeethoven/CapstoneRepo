using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRandomization : MonoBehaviour
{
    public List<GameObject> characters;
    //public Animator animator;
    //public bool test = false;
    public GameObject currentAvatar;

    //public CharacterRandomization(List<GameObject> characters)
    //{
    //    this.characters = characters;
    //}

    public void Start()
    {

        Debug.Log("Randomization script started");
        int x = Random.Range(0, characters.Count);//make dynamic by doing characters size instead of a constant number 5
        foreach (GameObject Char in characters)
        {
            if (Char.activeInHierarchy)
            {
                Char.SetActive(false);
            }
        }
        currentAvatar = characters[x];
        characters[x].SetActive(true);
        Debug.Log("the current character is number: " + x);
        //animator = characters[x].GetComponent<Animator>();
        //animator.SetTrigger("Idle");
    }

    //public void DelayAnimation()
    //{
    //    Debug.Log("Delay started");
    //    int z = Random.Range(4, 11);
    //    //StartCoroutine(DelayBotAction(z));
    //}

    //private void Update()
    //{
    //    if (test)
    //    {
    //        StartCoroutine(RunButtonPushinAnim());
    //        print("hi");
    //        test = false;
    //    }
    //}

    //IEnumerator DelayBotAction(int delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    StartCoroutine(RunButtonPushinAnim());
        
    //    //call the function of animation start
    //}

    //public void StartAnimation()
    //{
    //    //access the animator and call the necesary animation
    //    StartCoroutine(RunButtonPushinAnim());
    //}
    //private void ResetTrigger()
    //{
    //    animator.ResetTrigger("ButtonPush");
    //    animator.SetTrigger("Idle");
    //}

    //IEnumerator RunButtonPushinAnim()
    //{
    //    animator.SetBool("ButtonPush", true);
    //    yield return new WaitForSeconds(1.8f);
    //    animator.SetBool("ButtonPush", false);
    //}
}
