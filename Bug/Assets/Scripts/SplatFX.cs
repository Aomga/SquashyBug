using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip[] splatClips;

    public static Action PlayRandomSplat;
    void Start(){
        PlayRandomSplat += OnPlayRandomSplat;
    }

    void OnPlayRandomSplat(){
        audioSource.PlayOneShot(splatClips[UnityEngine.Random.Range(0, splatClips.Length)]);
    }
    
}
