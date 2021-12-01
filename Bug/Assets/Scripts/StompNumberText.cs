using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class StompNumberText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] RectTransform textField;

    public static int stomps = 0;

    void Start()
    {
        LevelManager.TotalStomps += SetStompAmount;
        Stomp.StompStarted += OnStompUsed;
    }

    void OnStompUsed(){
        if(!LevelManager.inMenu){
            SetStompAmount(stomps);
        }
    }

    void SetStompAmount(int setTo){
        stomps = setTo;
        text.text = stomps.ToString();
    }
}
