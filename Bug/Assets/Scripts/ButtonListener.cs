using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListener : MonoBehaviour
{
    public static Action SpacePressed;
    public static Action EscapePressed;
    public static Action EnterPressed;
    public static Action MPressed;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            SpacePressed?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            // EscapePressed?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.M)){
            MPressed?.Invoke();
        }
    }
}
