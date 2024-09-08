using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance; // Define the singleton instance

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Enforce singleton pattern
        }
    }

    public void endGame()
    {
        // Logic for ending the game, e.g., showing a Game Over screen
        Debug.Log("Game has ended!");
    }
}
