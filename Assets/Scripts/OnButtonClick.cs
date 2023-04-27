using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour
{
    public Button currentButton;
    public Button otherButton;
    public GameObject currentCanvas;
    public GameObject popupPanel;
    public TMPro.TMP_Text popupText;
    public GameObject opponentAvatar = null;
    public AvatarRandomizationManager avatarRandomizationManager;
    public Button coopButt;
    public Button defButt;

    private void Start()
    {
        coopButt.interactable = false;
        defButt.interactable = false;
        Debug.Log("Button script started");
        currentButton.onClick.AddListener(CallDelayBot);
        popupPanel.SetActive(false);
        avatarRandomizationManager = AvatarRandomizationManager.Instance;

        opponentAvatar = avatarRandomizationManager.GetOpponentAvatar();
    }

    public void CallDelayBot()
    {
        if (opponentAvatar == null)
        {
            opponentAvatar = avatarRandomizationManager.GetOpponentAvatar();
            Debug.Log(opponentAvatar);
        }
        if (currentButton.tag == "CooperateButton" || currentButton.tag == "DefectButton")
        {
            currentButton.interactable = false;
            otherButton.interactable = false;
            if (currentButton.tag == "CooperateButton")
            {
                opponentAvatar.GetComponent<AvatarButtonAnimationManager>().DelayAnimation(currentButton,  0);
            }
            else
            {
                opponentAvatar.GetComponent<AvatarButtonAnimationManager>().DelayAnimation(currentButton,  1);
            }
        }
        else if (currentButton.tag == "ContinueButton")
        {
            Debug.Log("Continue Button has been clicked");
            StartCoroutine(MenuDelayed());
            coopButt.interactable = true;
            defButt.interactable = true;
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
        yield return new WaitForSeconds(1f);
        CloseMenu();
    }
    public void ShowPopup(string message, bool gameEnd)
    {
        popupText.text = message;
        popupPanel.SetActive(true);
        StartCoroutine(HidePopupAfterDelay(gameEnd));
        //if (gameEnd)
        //{
        //    popupPanel.SetActive(false);
        //}
    }
    IEnumerator HidePopupAfterDelay(bool gameEnd)
    {
        yield return new WaitForSeconds(3.2f); // change the delay time as needed
        popupPanel.SetActive(false);
        if (!gameEnd)
        {
            currentButton.interactable = true;
            otherButton.interactable = true;
        }
        

    }
}
