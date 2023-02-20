using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area2Controller : XRSceneController
{
    public override void Init()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.hasVisitedArea2 = true;
        }
        
    }
}
