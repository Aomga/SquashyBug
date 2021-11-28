using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class CameraMover : MonoBehaviour
{
    [SerializeField] private  Vector3 initCoords;
    [SerializeField] private  GameObject[] levelCoords;
    [SerializeField] private  GameObject endScreenLevel;
    [SerializeField] private  AudioSource[] windSounds;
    [SerializeField] private float travelTime;
    [SerializeField] Ease ease = Ease.InOutExpo;

    public static Action<int> MoveToLevel;
    public static Action MoveToNextLevel;
    public static Action<Action> MoveToEndScreen;
    public static Action ArrivedAtNextLevel;
    void Awake(){
        transform.position = initCoords;
        currentLevel = 0;
        OnMoveToLevel(currentLevel);
        MoveToLevel += OnMoveToLevel;
        MoveToEndScreen += OnMoveToEndScreen;
    }

    private void OnMoveToLevel(int levelNumber){
        foreach (AudioSource windSound in windSounds)
        {   
            windSound.Play();
        }
        Vector3 newCoords = new Vector3(levelCoords[levelNumber].transform.position.x, 15, -15);
        transform.DOMove(newCoords, travelTime)
        .SetEase(ease)
        .OnComplete(() => {
            if(levelNumber != 0)
                ArrivedAtNextLevel?.Invoke();
        });

        MusicMixer.FadeInMusicState(levelNumber);
    }

    private void OnMoveToEndScreen(Action callback){
        foreach (AudioSource windSound in windSounds)
        {   
            windSound.Play();
        }
        Vector3 newCoords = new Vector3(endScreenLevel.transform.position.x, 15, endScreenLevel.transform.position.z - 15);
        transform.DOMove(newCoords, travelTime)
        .SetEase(ease)
        .OnComplete(() => {callback?.Invoke();});

        MusicMixer.FadeInMusicState(100);
    }

    private int currentLevel;
    private void NextLevel(){
        currentLevel++;

        if(currentLevel >= levelCoords.Length){
            currentLevel = 0;
        }

        OnMoveToLevel(currentLevel);
    }
}
