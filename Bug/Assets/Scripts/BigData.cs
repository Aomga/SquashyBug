using System;
using System.Collections.Generic;
using UnityEngine;

public class BigData : MonoBehaviour
{
    //Personal Bests
    private static List<int> bestLevelScores = new List<int>();
    private static List<float> bestLevelTimes = new List<float>(); 

    public static int totalLevels = 20;

    private void Start() {
        for (int i = 0; i < totalLevels; i++)
        {
            bestLevelScores.Add(0);
            bestLevelTimes.Add(999f);
        }

        OnLoadData();
    }

    private void OnLoadData(){
        LoadBestScores();
        LoadBestTimes();
    }

    public static void LoadBestScores(){
        for (int i = 0; i < totalLevels; i++)
        {
            if(!PlayerPrefs.HasKey("levelScore-" + i.ToString())){
                PlayerPrefs.SetInt("levelScore-" + i.ToString(), 0);
            }else{
                SetBestLevelScore(i, PlayerPrefs.GetInt("levelScore-" + i.ToString(), 0));
            }
        }
    }
    public static void LoadBestTimes(){
        for (int i = 0; i < totalLevels; i++)
        {
            if(!PlayerPrefs.HasKey("levelTime-" + i.ToString())){
                PlayerPrefs.SetFloat("levelTime-" + i.ToString(), 999f);
            }else{
                SetBestLevelTime(i, PlayerPrefs.GetFloat("levelTime-" + i.ToString(), 0));
            }
        }
    }

    public static int GetBestLevelScore(int level){
        return bestLevelScores[level];
    }
    public static float GetBestLevelTime(int level){
        return bestLevelTimes[level];
    }

    public static void SetBestLevelScore(int level, int score){
        bestLevelScores[level] = score;

        PlayerPrefs.SetInt("levelScore-" + level.ToString(), score);
    }
    public static void SetBestLevelTime(int level, float time){
        bestLevelTimes[level] = time;

        PlayerPrefs.SetFloat("levelTime-" + level.ToString(), time);
    }
    
    //CURRENT GAME TOTALS
    private static int gameTotalScore;
    public static int GetGameTotalScore(){
        return gameTotalScore;
    }
    public static void AddToGameTotalScore(int addBy){
        gameTotalScore += addBy;
        overallScore += addBy;
    }
    private static float gameTotalTime;
    public static float GetGameTotalTime(){
        return gameTotalTime;
    }
    public static void AddToGameTotalTime(float addBy){
        gameTotalTime += addBy;
        totalTime += addBy;
    }
    private static int gameTotalBugs;
    public static int GetGameTotalBugs(){
        return gameTotalBugs;
    }

    public static void IncrementTotalBugs(int incBy = 1){
        gameTotalBugs += incBy;
        totalBugsKilled += incBy;
    }

    private static int gameTotalStomps;
    public static int GetGameTotalStomps(){
        return gameTotalStomps;
    }

    public static void IncTotalStomps(){
        totalStomps++;
        gameTotalStomps++;
    }

    public static void ClearGameTotals(){
        gameTotalScore = 0;
        gameTotalTime = 0;
        gameTotalBugs = 0;
        gameTotalStomps = 0;
    }

    //BIG DATA tm
    private static int overallScore;
    private static float totalTime;
    private static int totalStomps;
    private static int totalBugsKilled;
    private static int totalGamesPlayed;

    public static void IncTotalGamesPlayed(){
        totalGamesPlayed++;
    }


}
