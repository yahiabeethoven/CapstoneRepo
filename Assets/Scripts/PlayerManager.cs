using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] 

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public bool hasVisitedArea2 = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = null;
        }
        else
        {
            Debug.LogError("too many Player Managers!");
            Destroy(this.gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
