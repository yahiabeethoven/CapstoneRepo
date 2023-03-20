using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour
{
    public Button currentButton;
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
        }
        else
        {
            Debug.Log("Defect Button has been pressed");
        }

        GameObject.Find("Y Bot@Button Pushing").GetComponent<AvatarButtonAnimationManager>().DelayAnimation();
    }
}
