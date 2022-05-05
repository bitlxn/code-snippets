using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [Space]
    [Header("Follow Point")] 
    [SerializeField] private BoxCollider2D triggerPoint;
    
    [Header("Player")]
    [SerializeField] public GameObject currentFollowingPlayer;
    [SerializeField] public bool foundCurrentPlayer;

    [Header("Positions")]
    [SerializeField] public Vector3 currentFollowingPlayerPosition;
    [SerializeField] private Vector3 defaultCameraPosition;

    [Header("Smooth")] 
    [SerializeField] private float movingSpeed;

    [Header("Intro")]
    [SerializeField] private bool hasPlayedIntro = false;
    [SerializeField] private float moveAmount;
    [Space] 
    [SerializeField] private float animationTime;
    [SerializeField] private AnimationCurve animationCurve;

    private void Start()
    {
        defaultCameraPosition = transform.position;

        triggerPoint = GetComponent<BoxCollider2D>();
        
    }

    private void Update()
    {
        if (levelManager.gameStarted)
        {
            if (!hasPlayedIntro)
            {
                LeanTween.moveY(gameObject, moveAmount, animationTime).setEase(animationCurve);
                hasPlayedIntro = true;
            }

            if (foundCurrentPlayer)
            {
                currentFollowingPlayerPosition = currentFollowingPlayer.transform.position;   
            
                if (currentFollowingPlayerPosition.y > transform.position.y)
                {
                    var movingPosition = new Vector3(defaultCameraPosition.x, currentFollowingPlayerPosition.y, defaultCameraPosition.z);

                    transform.position = Vector3.Lerp(transform.position, movingPosition, movingSpeed * Time.deltaTime);
                }
            }
        }

        var transform1 = transform;
        var position = transform1.position;
        Debug.DrawLine(new Vector3(-20, position.y, 0), new Vector3(20, position.y, 0), Color.green);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            currentFollowingPlayer = col.gameObject;
            foundCurrentPlayer = true;
        }
    }
}
