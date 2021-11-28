using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MusicMixer : MonoBehaviour
{
    
    [SerializeField] private MusicState[] musicStates;
    [SerializeField] private MusicState endScreenMusicState;

    [SerializeField] private float fadeTime = 5;

    public static Action<int> FadeInMusicState;
    public static Action FadeOutAll;

    void OnFadeInMusicState(int stateNumber){

        //end screen    
        if(stateNumber == 100){
            FadeInTracks(endScreenMusicState.GetAudioSources());
        }
        
        if(stateNumber == 0 || stateNumber == 1){
            FadeOutAll();
        }
        
        if(stateNumber > 0){
            FadeOutTracks(musicStates[stateNumber - 1].GetAudioSources());
        }

        FadeInTracks(musicStates[stateNumber].GetAudioSources());
    }

    private void FadeInTracks(AudioSource[] tracks){
        foreach (AudioSource track in tracks)
        {
            track.DOFade(1, fadeTime);
        }
    }

    private void FadeOutTracks(AudioSource[] tracks){
        foreach (AudioSource track in tracks)
        {
            track.DOFade(0, fadeTime);
        }
    }

    private void OnFadeOutAll(){
        foreach (MusicState state in musicStates)
        {
            FadeOutTracks(state.GetAudioSources());
        }
    }

    void Awake(){
        FadeInMusicState += OnFadeInMusicState;
        FadeOutAll += OnFadeOutAll;
    }

}
