using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public static test Instance { get; private set; }
    private void Awake()
    {
        if (Instance!=this && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Testing()
    {
        print("succeed");
    }
}
