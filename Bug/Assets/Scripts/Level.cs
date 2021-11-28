using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    public int totalStomps;

    private int totalBugs;
    private int totalBugsKilled;
    private int totalPoints;
    private float totalTime;

    void Start(){
        LevelManager.LevelStarting += GetTotalBugs;
        LevelManager.LevelComplete += RemoveBugKilledListener;
        LevelManager.AllStompsUsed += RemoveBugKilledListener;
    }


    void RemoveBugKilledListener(){
        if(LevelManager.GetCurrentLevel() == levelNumber){
            LevelManager.BugKilled -= OnBugKilled;
        }
    }


    void GetTotalBugs(){
        if(LevelManager.GetCurrentLevel() == levelNumber){
            totalBugsKilled = 0;
            totalBugs = GetComponentsInChildren<KillBug>().Length;
            LevelManager.BugKilled += OnBugKilled;
        }
    }

    void OnBugKilled(){
        totalBugsKilled++;
        if(totalBugsKilled >= totalBugs){
            LevelManager.BugKilled -= OnBugKilled;
            LevelManager.AllBugsDead?.Invoke();
        }
    }
}
