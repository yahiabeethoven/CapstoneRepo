using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour
{
    public Button currentButton;
    public Button otherButton;
    public GameObject currentCanvas;

    private void Start()
    {
        Debug.Log("Button script started");
        currentButton.onClick.AddListener(CallDelayBot);
    }

    public void CallDelayBot()
    {
        if (currentButton.tag == "CooperateButton" || currentButton.tag == "DefectButton")
        {
            currentButton.interactable = false;
            otherButton.interactable = false;
            if (currentButton.tag == "CooperateButton")
            {
                GameObject.Find("Y Bot@Button Pushing").GetComponent<AvatarButtonAnimationManager>().DelayAnimation(currentButton, otherButton, 0);
            }
            else
            {
                GameObject.Find("Y Bot@Button Pushing").GetComponent<AvatarButtonAnimationManager>().DelayAnimation(currentButton, otherButton, 1);
            }
        }
        else if (currentButton.tag == "ContinueButton")
        {
            Debug.Log("Continue Button has been clicked");
            StartCoroutine(MenuDelayed());
            return;
        }
    }
    public void CloseMenu()
    {
        DestroyImmediate(currentCanvas, true);
        Debug.Log("canvas destroyed");
    }
    IEnumerator MenuDelayed()
    {
        yield return new WaitForSeconds(1.5f);
        CloseMenu();
    }
}
