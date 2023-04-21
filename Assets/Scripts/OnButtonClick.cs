using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnButtonClick : MonoBehaviour
{
    public Button currentButton;
    public Button otherButton;
    public GameObject currentCanvas;
    public GameObject popupPanel;
    public TMPro.TMP_Text popupText;

    private GameObject opponentAvatar;

    private void Start()
    {
        //Debug.Log("Button script started");
        currentButton.onClick.AddListener(CallDelayBot);
        popupPanel.SetActive(false);

        if (AvatarRandomizationManager.Instance != null && AvatarRandomizationManager.Instance.GetOpponentAvatar() != null)
        {
            opponentAvatar = AvatarRandomizationManager.Instance.GetOpponentAvatar();
        }
    }

    public void CallDelayBot()
    {
        //if (opponentAvatar == null && AvatarRandomizationManager.Instance != null && AvatarRandomizationManager.Instance.GetOpponentAvatar() != null)
        //{
        //    opponentAvatar = AvatarRandomizationManager.Instance.GetOpponentAvatar();
        //}

        opponentAvatar = AvatarRandomizationManager.Instance.GetOpponentAvatar();

        if (currentButton != null && otherButton != null && (currentButton.tag == "CooperateButton" || currentButton.tag == "DefectButton"))
        {
            currentButton.interactable = false;
            otherButton.interactable = false;
            if (currentButton.tag == "CooperateButton")
            {
                opponentAvatar.GetComponent<AvatarButtonAnimationManager>().DelayAnimation(currentButton, otherButton, 0);
            }
            else
            {
                opponentAvatar.GetComponent<AvatarButtonAnimationManager>().DelayAnimation(currentButton, otherButton, 1);
            }
        }
        else if (currentButton != null && currentButton.tag == "ContinueButton")
        {
            Debug.Log("Continue Button has been clicked");
            StartCoroutine(MenuDelayed());
            return;
        }
    }

    public void CloseMenu()
    {
        Destroy(currentCanvas);
        Debug.Log("canvas destroyed");
    }

    IEnumerator MenuDelayed()
    {
        yield return new WaitForSeconds(1.5f);
        CloseMenu();
    }

    public void ShowPopup(string message)
    {
        if (popupText != null)
        {
            popupText.text = message;
            popupPanel.SetActive(true);
            StartCoroutine(HidePopupAfterDelay());
        }
    }

    IEnumerator HidePopupAfterDelay()
    {
        yield return new WaitForSeconds(3.2f); // change the delay time as needed
        popupPanel.SetActive(false);
    }
}
