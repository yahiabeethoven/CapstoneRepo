using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FirstInstructions : MonoBehaviour
{
    public Button currentButton;
    public GameObject currentCanvas;
    public AvatarRandomizationManager avatarRandomizationManager;

    private void Start()
    {
        Debug.Log("Button script started");
        currentButton.onClick.AddListener(CallDelayBot);
        avatarRandomizationManager = AvatarRandomizationManager.Instance;
        StartCoroutine(StartDelayed());
    }

    public void CallDelayBot()
    {
        Debug.Log("Continue Button has been clicked");
        StartCoroutine(MenuDelayed());
        return;
    }
    public void CloseMenu()
    {
        DestroyImmediate(currentCanvas, true);
        Debug.Log("canvas destroyed");
    }
    IEnumerator MenuDelayed()
    {
        yield return new WaitForSeconds(1f);
        CloseMenu();
    }

    private void init()
    {
        transform.GetComponent<RectTransform>().DOAnchorPosY(-150, 1f);
    }
    IEnumerator StartDelayed()
    {
        yield return new WaitForSeconds(3f);
        init();
    }
}
