using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour
{
    public Button cooperateButton;
    public Button defectButton;
    private void Start()
    {
        Debug.Log("Button script started");
        cooperateButton.onClick.AddListener(CallDelayBot);
        defectButton.onClick.AddListener(CallDelayBot);
    }

    public void CallDelayBot()
    {
        Debug.Log("Button has been pressed");
        GameObject.Find("Characters").GetComponent<CharacterRandomization>().DelayAnimation();
    }
}
