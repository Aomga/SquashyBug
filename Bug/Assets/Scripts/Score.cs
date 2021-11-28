using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Action<int> IncreaseScore;
    public static Action<int> IncreaseTotalScore;
    public static Action ResetScore;

    private static int totalScore = 0;

    private void Start() {
        totalScore = 0;
        IncreaseScore += OnIncreaseScore;
        IncreaseTotalScore += OnIncreaseTotalScore;
        ResetScore += OnResetScore;
    }

    private void OnIncreaseScore(int increaseBy){
        totalScore += increaseBy;
    }

    private void OnIncreaseTotalScore(int increaseBy){
        BigData.AddToGameTotalScore(increaseBy);
    }

    public void OnResetScore(){
        totalScore = 0;
    }

    public static int GetScore(){
        return totalScore;
    }

}
