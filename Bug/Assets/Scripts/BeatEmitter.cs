using System;
using UnityEngine;

public class BeatEmitter : MonoBehaviour
{
    public static Action Beat;
    public static Action HalfBeat;

    public void EmitBeat(){
        Beat?.Invoke();
        Debug.Log("Beat");
    }

    public void EmitHalfBeat(){
        HalfBeat?.Invoke(); 
        Debug.Log("Half Beat");
    }
}
