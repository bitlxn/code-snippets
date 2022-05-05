using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] public bool gameStarted = false;

    public void StartGame()
    {
        gameStarted = true;
    }

    public void EndGame()
    {
        gameStarted = false;
    }
}
