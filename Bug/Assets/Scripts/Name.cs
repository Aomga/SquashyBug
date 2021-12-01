using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Name : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;

    [SerializeField] Image bg;
    [SerializeField] TextMeshProUGUI[] texts;

    [SerializeField] Image[] images;


    public static bool NameSaved;
    public static string PlayerName;

    public static Action GetName;

    private bool listeningForEnter;
    [SerializeField] TextMeshProUGUI pressEnterText;


    void Start(){
        if(PlayerPrefs.HasKey("name_00")){
            NameSaved = true;
            PlayerName = PlayerPrefs.GetString("name_00");
            if(PlayerName == ""){
                PlayerName = "somehowNoName";
            }

            SceneManager.LoadScene("Game");
        }else{
            OnGetName();
        }
    }

    void Update(){
        if(listeningForEnter){
            if(Input.GetKeyDown(KeyCode.Return)){
                AcceptName();
            }
        }
    }

    void OnGetName(){
        bg.DOFade(1f, 0);

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].DOFade(1f, 0.5f);
        }
        for (int i = 0; i < images.Length; i++)
        {
            images[i].DOFade(1f, 0.25f);
        }
    }

    void HideLeaderboard(){
        bg.DOFade(0f, 0.5f).SetDelay(1f).OnComplete(() => {SceneManager.LoadScene("Game");});

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].DOFade(0f, 0.25f);
        }

        for (int i = 0; i < images.Length; i++)
        {
            images[i].DOFade(0f, 0.25f);
        }

        pressEnterText.DOFade(0f, 0.25f);
    }

    public void NameInputEntered(string _){
        if(nameInput.text.Length >= 1){
            listeningForEnter = true;
            pressEnterText.DOFade(1f, 0.5f);
        }else {
            listeningForEnter = false;
            pressEnterText.DOFade(0f, 0.5f);
        }
    }

    private void AcceptName(){
        listeningForEnter = false;
        NameSaved = true;
        PlayerName = nameInput.text;

        PlayerPrefs.SetString("name_00", PlayerName);

        HideLeaderboard();
    }
}

