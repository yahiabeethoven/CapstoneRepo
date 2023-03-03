using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRandomization : MonoBehaviour
{
    public List<GameObject> characters;
    public Animator animator;
    //public GameObject currentAvatar;

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
        //currentAvatar = characters[x];
        characters[x].SetActive(true);
        animator = characters[x].GetComponent<Animator>();
    }

    public void DelayAnimation()
    {
        int z = Random.Range(4, 11);
        StartCoroutine(DelayBotAction(z));
    }

    IEnumerator DelayBotAction(int delay)
    {
        yield return new WaitForSeconds(delay);
        StartAnimation();
        //call the function of animation start
    }

    public void StartAnimation()
    {
        //access the animator and call the necesary animation
        animator.SetTrigger("ButtonPush");
        Invoke(nameof(ResetTrigger), animator.GetCurrentAnimatorStateInfo(0).length);
    }
    private void ResetTrigger()
    {
        animator.ResetTrigger("ButtonPush");
    }

}
