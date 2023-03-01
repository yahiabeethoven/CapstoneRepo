using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour
{
    public Button userButton;
    private void Start()
    {
        userButton.onClick.AddListener(CallDelayBot);
    }

    void CallDelayBot()
    {
        GameObject.Find("Characters").GetComponent<CharacterRandomization>().DelayAnimation();
    }
}
