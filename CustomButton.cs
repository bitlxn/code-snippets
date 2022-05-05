using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : Button
{
    private void OnMouseEnter()
    {
        this.Select();
    }
}
