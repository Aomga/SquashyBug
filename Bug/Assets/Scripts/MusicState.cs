using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicState : MonoBehaviour
{
    [SerializeField] public AudioSource[] audioSources;

    public AudioSource[] GetAudioSources(){
        return audioSources;
    }
}
