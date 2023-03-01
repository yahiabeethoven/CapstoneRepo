using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    private InputAction _thumbstick;
    // Start is called before the first frame update
    void Start()
    {
        //var activate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        //activate.Enable();

        //var cancel = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Cancel");
        //cancel.Enable();

        //_thumbstick = actionAsset.FindActionMap("XRI LeftHand").FindAction("");
        //_thumbstick.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
