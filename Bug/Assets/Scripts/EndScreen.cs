using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] Image bg;
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI totalBugsText;

    void Start(){
        LevelManager.GameComplete += OnGameComplete;

        ButtonListener.SpacePressed += RestartGame;
        ButtonListener.EscapePressed += QuitGame;
    }

    void RestartGame(){
        if(listeningForButtonPresses){
            listeningForButtonPresses = false;
            LevelManager.StartGame?.Invoke();
            HideEndScreen();
        }
    }

    void QuitGame(){
        //byebye sound
        Application.Quit();
    }

    bool listeningForButtonPresses = false;

    void OnGameComplete(){
        bg.DOFade(0.9f, 1f);

        scoreText.text = "Score: <color=#F25F5C><bounce>" + BigData.GetGameTotalScore().ToString() + "</bounce></color>";
        timeText.text = "Time: <color=#F25F5C><bounce>" + BigData.GetGameTotalTime().ToString("0.00") + "</bounce></color>";
        totalBugsText.text = "Bugs Killed: <color=#F25F5C><bounce>" + BigData.GetGameTotalBugs().ToString() + "</bounce></color>";

        for (int i = 0; i < texts.Length; i++)
        {
            int delay = i;

            //increase delay for last two texts (button presses)
            if(i >= texts.Length - 2){
                delay += 1;
            }

            if(i >= texts.Length - 1){
                texts[i].DOFade(1f, 1f).SetDelay(delay).OnComplete(() => {listeningForButtonPresses = true;});
            }else{
                texts[i].DOFade(1f, 1f).SetDelay(delay);
            }
        }
    }

    void HideEndScreen(){
        bg.DOFade(0f, 0.5f);
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].DOFade(0f, 0.25f);
        }
    }
}
