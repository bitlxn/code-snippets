using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] public string applicationVersion = "Error";
    [SerializeField] public string unityVersion = "Error";

    private void Awake()
    {
        applicationVersion = Application.version.ToString();
        unityVersion = Application.unityVersion.ToString();
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.LogWarning("Sorry, This is not a build you cant quit here.");
    }

    public void FullscreenApplication()
    {   
        // Toggle fullscreen
        Screen.fullScreen = !Screen.fullScreen;
    }
}
