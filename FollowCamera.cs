using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [Space]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float movingSpeed;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.gameStarted)
        {
            transform.position = Vector3.Lerp(transform.position, playerCamera.position, movingSpeed * Time.deltaTime);
        }
    }
}
