using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour
{
    public Button currentButton;
    public GameObject currentCanvas;
    //public Button defectButton;
    private void Start()
    {
        Debug.Log("Button script started");
        currentButton.onClick.AddListener(CallDelayBot);
    }

    public void CallDelayBot()
    {
        if (currentButton.tag == "CooperateButton")
        {
            Debug.Log("Cooperate Button has been pressed");
            GameObject.Find("Y Bot@Button Pushing").GetComponent<AvatarButtonAnimationManager>().DelayAnimation();
        }
        else if (currentButton.tag == "DefectButton")
        {
            Debug.Log("Defect Button has been pressed");
            GameObject.Find("Y Bot@Button Pushing").GetComponent<AvatarButtonAnimationManager>().DelayAnimation();
        }
        else
        {
            Debug.Log("Continue Button has been clicked");
            if (currentCanvas.activeInHierarchy)
            {
                currentCanvas.SetActive(false);
            }
        }

        
    }
}
