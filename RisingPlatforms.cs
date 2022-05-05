using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatforms : MonoBehaviour
{
    [Header("Animation")] 
    [SerializeField] private Vector3 moveAmount;
    [SerializeField] private float animationTime;
    [SerializeField] private AnimationCurve animationCurve;
    [Space] 
    [SerializeField] private bool hasPlayed = false;

    [Header("Game-object")] 
    [SerializeField] private GameObject moveObject;

    [Header("Managers")] 
    [SerializeField] private LevelManager levelManager;

    private void Update()
    {
        if (!levelManager.gameStarted) return;
        if (hasPlayed) return;
        LeanTween.move(moveObject, moveAmount, animationTime).setEase(animationCurve);
        hasPlayed = true;
    }
}
