using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class LeaderboardManager : MonoBehaviour
{
    //PHP URLs
    [Header("Connection Details")]
    [SerializeField] string databaseName;
    [SerializeField] string databaseUsername;
    [SerializeField] string databasePassword;
    [Space]
    [SerializeField] string postURL;
    [SerializeField] string retrieveURL;
    [SerializeField] string table;
    [SerializeField] int numberOfScoresToRetrieve;

    //actions to call 
    public static UnityAction PostScoreToDB;
    public static UnityAction GetScoreFromDB;
    public static UnityAction UpdateHighScoreText;
    public static UnityAction UpdateScoreText;
    public static UnityAction UpdateBestScoreText;

    public static UnityAction<Dictionary<string, int>> RetrievedScores;

    //subscribe to actions
    private void OnEnable()
    {
        PostScoreToDB += OnPostScoreToDB;
        GetScoreFromDB += OnGetScoreFromDB;
    }

    private void OnDisable()
    {
        PostScoreToDB -= OnPostScoreToDB;
        GetScoreFromDB -= OnGetScoreFromDB;
    }

    private void Start()
    {
        LevelManager.GameComplete += OnPostScoreToDB;

        OnGetScoreFromDB();
    }

    //buffer functions for the coroutines
    private void OnPostScoreToDB()
    {
        StartCoroutine(
            SubmitScore(
                BigData.GetGameTotalScore(), 
                PlayerPrefs.GetString("name_00", "no name"), 
                BigData.GetGameTotalTime(), 
                BigData.GetGameTotalBugs(),
                BigData.GetGameTotalStomps()
            ));
    }

    private void OnGetScoreFromDB()
    {
        StartCoroutine(GetScores());
    }

    //handles posting the score to your DB on success will get scores
    IEnumerator SubmitScore(float score, string name, float totalTime, int totalBugs, int totalStomps)
    {
        var form = new WWWForm();

        form.AddField("db", databaseName);
        form.AddField("user", databaseUsername);
        form.AddField("pass", databasePassword);
        form.AddField("table", table);
        form.AddField("name", name);
        form.AddField("score", score.ToString());
        form.AddField("time_played", totalTime.ToString());
        form.AddField("bugs_killed", totalBugs.ToString());
        form.AddField("total_stomps", totalStomps.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(postURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                // Debug.Log(www.error); //if you're having issues uncomment this and check your log
            }
            else
            {
                // Debug.Log(www.downloadHandler.text);
                // Debug.Log("submitted");
                OnGetScoreFromDB();
            }
        }
    }

    //pulls score from your database and updates your highscore textfield
    IEnumerator GetScores()
    {
        var form = new WWWForm();
        form.AddField("db", databaseName);
        form.AddField("user", databaseUsername);
        form.AddField("pass", databasePassword);
        form.AddField("table", table);
        form.AddField("limit", numberOfScoresToRetrieve);

        using (UnityWebRequest www = UnityWebRequest.Post(retrieveURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                // Debug.Log(www.error); //if you're having issues uncomment this and check your log
            }
            else
            {
                // Debug.Log("Got: " + www.downloadHandler.text);
                RetrievedScores?.Invoke(ParseLeaderboard(www.downloadHandler.text));
            }
        }
    }

    public Dictionary<string, int> ParseLeaderboard(string leaderboardString){

        Dictionary<string, int> leaderboardDictionary = new Dictionary<string, int>();
        
        leaderboardDictionary.Clear();

        string[] splitLeaderboardString = leaderboardString.Split(new char[] { ',' });

            //minus 1 because the last split is always empty
        for (int i = 0; i < splitLeaderboardString.Length - 1; i++)
        {
            string[] splitNameAndScore = splitLeaderboardString[i].Split(new char[] {'|'});
            leaderboardDictionary.Add(splitNameAndScore[0], int.Parse(splitNameAndScore[1]));
        }
        
    return leaderboardDictionary;
    }
}
