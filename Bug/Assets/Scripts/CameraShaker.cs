using System;
using UnityEngine;
using DG.Tweening;

public class CameraShaker : MonoBehaviour
{
    public static Action shakeLight;
    public static Action shakeHeavy;

    [SerializeField] float lightShakeDuration;
    [SerializeField] float lightShakeStrength = 3;
    [SerializeField] int lightShakeVibrato = 10;
    [SerializeField] float lightShakeRandomness = 90;

    [SerializeField] float heavyShakeDuration;
    [SerializeField] float heavyShakeStrength = 3;
    [SerializeField] int heavyShakeVibrato = 10;
    [SerializeField] float heavyShakeRandomness = 90;

    void Awake(){
        shakeLight += OnShakeLight;
        shakeHeavy += OnShakeHeavy;
    }

    private void OnShakeLight(){
        Camera.main.DOShakePosition(lightShakeDuration, lightShakeStrength, lightShakeVibrato, lightShakeRandomness);
    }

    private void OnShakeHeavy(){
        Camera.main.DOShakePosition(heavyShakeDuration, heavyShakeStrength, heavyShakeVibrato, heavyShakeRandomness);

    }
}
