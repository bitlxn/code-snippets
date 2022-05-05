using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [Space]
    [SerializeField] private Animator buttonAnimationController;
    [Space] 
    [SerializeField] public bool buttonIsOn = false;

    private void OnCollisionEnter2D(Collision2D col)
    {
        buttonIsOn = true;
        buttonAnimationController.SetBool("isPressed", true);
        levelManager.gameStarted = true;
    }
}
