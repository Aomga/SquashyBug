using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class LeadboardPopulator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI names, scores, playerName;
    void Start(){
        LeaderboardManager.RetrievedScores += OnRetrievedScores;

        LevelManager.GameComplete += OnGameComplete;
        LevelManager.StartGame += HideLeaderboard;

    }

    void OnRetrievedScores(Dictionary<string, int> scoreDictionary){
        playerName.text = "<wave>" + Name.PlayerName + "</wave>";

        names.text = "<bounce>";
        scores.text = "<bounce>";
        foreach (KeyValuePair<string, int> dictionary in scoreDictionary)
        {
            names.text += dictionary.Key + ":" + '\n';
            scores.text += dictionary.Value.ToString() + '\n';
        }
        names.text += "</bounce>";
        scores.text += "</bounce>";
    }

    [SerializeField] Image bg;
    [SerializeField] TextMeshProUGUI[] texts;

    void OnGameComplete(){
        bg.DOFade(0.9f, 1f);

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].DOFade(1f, 1f).SetDelay(i);
        }
    }

    void HideLeaderboard(){
        bg.DOFade(0f, 0.5f);
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].DOFade(0f, 0.25f);
        }
    }
}
