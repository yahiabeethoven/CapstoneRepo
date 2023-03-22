using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestingTween : MonoBehaviour
{
    //private void Start()
    //{
        
    //}
    private void Update()
    {

        Debug.Log(GameObject.FindGameObjectWithTag("Transporter").GetComponent<TransporterController>().destination);
        if (GameObject.FindGameObjectWithTag("Transporter").GetComponent<TransporterController>().destination == "Area 1")
        {
            StartCoroutine(StartDelayed());
            return;
        }
    }
    private void init()
    {
        transform.GetComponent<RectTransform>().DOAnchorPosY(0, 1f);
    }
    IEnumerator StartDelayed()
    {
        yield return new WaitForSeconds(3f);
        init();
    }
}
