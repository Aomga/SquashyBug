using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class LevelTimer : MonoBehaviour
{
    private static float totalTime = 0;
    private bool timing = false;

    public static Action StartTimer;
    public static Action StopTimer;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] RectTransform textField;

    void Start(){
        StartTimer += OnStartTimer;
        StopTimer += OnStopTimer;
    }

    void Update()
    {
        if(timing){
            totalTime += Time.deltaTime;
            text.SetText(totalTime.ToString("0.00"));
        }
    }

    void OnStartTimer(){
        totalTime = 0;
        textField.DOShakeScale(0.2f);
        timing = true;
    }

    void OnStopTimer(){
        timing = false;
        BigData.AddToGameTotalTime(totalTime);

        text.SetText("");
    }

    public static float GetTime(){
        return totalTime;
    }
}
