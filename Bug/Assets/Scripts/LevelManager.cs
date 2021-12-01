using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Level[] levels;
    private static int currentLevel = 0;
    public static int GetCurrentLevel(){ return currentLevel; }

    private static bool levelStarted;

    public static Action LevelStarting;
    [SerializeField] AudioSource levelStartAudio;
    [SerializeField] AudioSource levelEndAudio;

    public static Action<int> TotalStomps;
    
    public static Action KillAllBugs;
    public static Action BugKilled;
    public static Action<int> TotalBugKilledInStomp;
    public static Action<int> BugBonusFromStomp;
    public static Action AllBugsDead;

    public static Action AllStompsUsed;
    public static Action LevelComplete;
    public static Action GameComplete;
    public static Action StartGame;

    public static bool inMenu;

    public void Start(){
        StartGame += OnStartGame;
        LevelComplete += OnLevelComplete;
        CameraMover.ArrivedAtNextLevel += PrepareNextLevel;

        AllBugsDead += OnAllBugsDead;
        AllStompsUsed += OnAllStompsUsed;

        Stomp.StompStarted += OnStompStarted;
        Stomp.StompEnded += OnStompEnded;

        inMenu = true;
    }

    //used when pressing play button / restarting game at end
    void OnStartGame(){
        inMenu = false;
        BigData.ClearGameTotals();
        Score.ResetScore?.Invoke();

        currentLevel = 0;
        allbugsKilled = false;
        MoveToNextLevel();
    }

    void MoveToNextLevel(){
        currentLevel++;

        if(currentLevel >= levels.Length){
            KillAllBugs?.Invoke();
            CameraMover.MoveToEndScreen?.Invoke(GameComplete);
        }else{
            ClearAndSpawnBugs();
            CameraMover.MoveToLevel?.Invoke(currentLevel);
        }
    }

    void PrepareNextLevel(){
        PopupMessage.DisplaySkipableMessage(
            "<b>Get Ready!</b> \n You have <color=#F25F5C><b> " 
            + levels[currentLevel].totalStomps 
            + "</b></color> stomps!",
            OnLevelStart);
    }

    void OnLevelStart(){
        LevelStarting?.Invoke();
        LevelTimer.StartTimer.Invoke();
        levelStartAudio.Play();
        Score.ResetScore?.Invoke();
        levelStarted = true;
        allbugsKilled = false;
        allStompsUsed = false;
        TotalStomps?.Invoke(levels[currentLevel].totalStomps);
    }

    void OnLevelComplete(){
        ShowLevelComplete("Level Complete!");
    }

    void OnAllBugsDead(){
        allbugsKilled = true;
        StompNumberText.stomps = 0;
        // ShowLevelComplete("All Bugs Squished!");
    }

    int bugsKilledInStomp = 0;
    void OnStompStarted(){
        bugsKilledInStomp = 0;
        BugKilled += IncrementBugsKilledInStomp;
    }

    bool allbugsKilled = false;
    bool allStompsUsed = false;

    void OnStompEnded(){
        if(bugsKilledInStomp > 0){
            int score = bugsKilledInStomp;
            int bonus = bugsKilledInStomp * bugsKilledInStomp;

            Score.IncreaseScore(score);
            TotalBugKilledInStomp?.Invoke(bugsKilledInStomp);

            if(bugsKilledInStomp > 2){
                Score.IncreaseScore(bonus);
                BugBonusFromStomp?.Invoke(bonus);
            }
        }

        //now do the check to see if levels over
        if(allbugsKilled){
            Invoke("ShowCompleteBugs", 0.5f);
        }

        if(allStompsUsed && !allbugsKilled){
            Invoke("ShowCompleteStomps", 0.5f);
        }

        BugKilled -= IncrementBugsKilledInStomp;
    }

    void ShowCompleteStomps(){
        ShowLevelComplete("Out of Stomps!");
    }

    void ShowCompleteBugs(){
        ShowLevelComplete("All Bugs Squished!");
    }

    void IncrementBugsKilledInStomp(){
        bugsKilledInStomp++;
    }

    void OnAllStompsUsed(){
        if(!inMenu){
            allStompsUsed = true;
        }
        // if(!allbugsKilled && !inMenu){
            // ShowLevelComplete("Out of Stomps!");
        // }
    }

    void ShowLevelComplete(string message){
        LevelTimer.StopTimer.Invoke();
        levelStarted = false;

        levelEndAudio.Play();
        
        //score with time bonus
        gotBonus = false;
        int timeBonus = CalculateTimeBonus();
        int bugBonus = CalculateBugBonus();

        bool bestScoreUpdated = false;
        if(BigData.GetBestLevelScore(currentLevel) < Score.GetScore() + timeBonus + bugBonus){
            BigData.SetBestLevelScore(currentLevel, Score.GetScore() + timeBonus + bugBonus);
            bestScoreUpdated = true;
        }
        string bestScoreText = bestScoreUpdated ? "<color=#3772FF><size=50%> new best score!</size></color>" : "<size=50%>best: <color=#3772FF>" + BigData.GetBestLevelScore(currentLevel) + "</color></size>";
        
        bool bestTimeUpdated = false;
        if(BigData.GetBestLevelTime(currentLevel) > LevelTimer.GetTime()){
            BigData.SetBestLevelTime(currentLevel, LevelTimer.GetTime());
            bestTimeUpdated = true;
        }
        string bestTimeText = bestTimeUpdated ? "<color=#3772FF><size=50%> new best time!</size></color>" : "<size=50%>best: <color=#3772FF>" + BigData.GetBestLevelTime(currentLevel).ToString("0.00") + "</color></size>";

        string bonusMessage = gotBonus 
            ? "</color><size=50%><color=#247BA0><color=#F25F5C>+" + timeBonus + "</color> time bonus!</color></size>\n" 
            : "";

        string bugKillBonus = allbugsKilled ? "<size=50%><color=#247BA0><color=#F25F5C>+" + bugBonus + "</color> all bugs bonus!</color></size> \n" : "";

        PopupMessage.DisplaySkipableMessage(
            message + " \n" 
            + "Level Score: <color=#F25F5C>" + (Score.GetScore() + timeBonus + bugBonus).ToString()+ "</color> \n"
            + bestScoreText + "\n"
            + "<size=50%><color=#247BA0><color=#F25F5C>" + Score.GetScore() + "</color> raw score</color></size> \n" 
            + bugKillBonus
            + bonusMessage
            + "Time: <color=#F25F5C>" + LevelTimer.GetTime().ToString("0.00") + "</color> \n"
            + bestTimeText
            , MoveToNextLevel);
    }

    int CalculateBugBonus(){
        if(!allbugsKilled) return 0;
        int bonus = levels[currentLevel].totalStomps * 10;
        Score.IncreaseTotalScore?.Invoke(Score.GetScore() + bonus);
        return bonus;
    }

    bool gotBonus = false;
    int CalculateTimeBonus(){
        float timeBonusBase = levels[currentLevel].totalStomps * 10;

        int bonus = (int)(timeBonusBase - LevelTimer.GetTime());

        if(bonus <= 0){
            bonus = 1;
        }else{
            gotBonus = true;
        }

        if(!gotBonus) return 0;

        Score.IncreaseTotalScore?.Invoke(Score.GetScore() + bonus);

        return bonus;
    }

    void ClearAndSpawnBugs(){
        //Clear out all existing bugs first
        KillAllBugs?.Invoke();

        //spawn bugs at next level
        CreatePathFollowers[] bugSpawners =  levels[currentLevel].GetComponentsInChildren<CreatePathFollowers>();
        foreach (CreatePathFollowers spawner in bugSpawners)
        {
            spawner.SpawnPathFollowers();
        }

        JumpyBugSpawner[] jumpSpawners = levels[currentLevel].GetComponentsInChildren<JumpyBugSpawner>();
        foreach (JumpyBugSpawner spawner in jumpSpawners)
        {
            spawner.SpawnJumpyBugs();
        }
    }

    public static bool StompingAllowed(){
        return levelStarted;
    }
}
