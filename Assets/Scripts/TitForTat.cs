using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitForTat : MonoBehaviour
{
    // Keep track of the player's previous move
    public int previousMove = 0;

    void Start()
    {
        // Initialize the previous move to cooperate
        previousMove = 0;
    }

    // Function to get the current move
    public int GetMove()
    {
        // If the previous move was to cooperate, also cooperate
        if (previousMove == 0)
        {
            return 0;
        }
        // If the previous move was to betray, also betray
        else
        {
            return 1;
        }
    }

    // Function to set the previous move
    public void SetMove(int move)
    {
        previousMove = move;
    }
}
