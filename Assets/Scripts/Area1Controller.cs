using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Area1Controller : XRSceneController
{
    public Transform xrRigOrigin2;
    public XRSocketInteractor keyCardSocket;
    public XRBaseInteractable keyCard;

    public override void Init()
    {
        if (PlayerManager.Instance)
        {
            if (PlayerManager.Instance.hasVisitedArea2)
            {
                keyCardSocket.startingSelectedInteractable = keyCard;
            }
        }
               
    }
    public override Transform GetXRRigOrigin()
    {
        if (PlayerManager.Instance)
        {
            return PlayerManager.Instance.hasVisitedArea2 ? xrRigOrigin2 : xrRigOrigin;
        }
        return null;
        
    }
}
