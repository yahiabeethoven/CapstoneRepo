using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestingTween : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartDelayed());
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
