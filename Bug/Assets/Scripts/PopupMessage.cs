using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class PopupMessage : MonoBehaviour
{
    [SerializeField] private Image banner;
    [SerializeField] private RectTransform textField;
    [SerializeField] private TextMeshProUGUI text;

    public static Action<string, float, Action> DisplayMessage;
    public static Action<string, Action> DisplaySkipableMessage;


    [SerializeField] float bannerFadeTime = 5f;
    [SerializeField] float textMoveTime = 5f;

    private Action storedCallback;
    private bool listeningForSpace;

    void Start(){
        DisplayMessage += OnDisplayMessage;
        DisplaySkipableMessage += OnDisplaySkipableMessage;
        ButtonListener.SpacePressed += CloseMessage;
    }

    private void OnDisplayMessage(string message, float textHoldTime = 2f, Action callback = null){
        text.text = "<bounce>" + message + "</bounce>";
        textField.anchoredPosition = new Vector3(100f, 0, 0);

        banner.DOFade(0.7f, bannerFadeTime)
        .SetEase(Ease.OutCirc);

        text.DOFade(1f, textMoveTime / 2);
        textField.DOAnchorPosX(0, textMoveTime)
        .SetEase(Ease.OutCirc)
        .OnComplete(() => {
            text.DOFade(0f, textMoveTime / 2).SetDelay(textHoldTime);
            textField.DOAnchorPosX(-100f, textMoveTime)
            .SetEase(Ease.InCirc)
            .SetDelay(textHoldTime)
            .OnComplete(() => {
                callback?.Invoke();
            });

            banner.DOFade(0f, bannerFadeTime)
            .SetEase(Ease.InCirc)
            .SetDelay(textHoldTime);
        });
    }

    private void OnDisplaySkipableMessage(string message, Action callback = null){
        text.text = "<bounce>" + message + "</bounce> \n" + "<wave a=0.5><color=#000><size=50%>press <b>SPACE</b> to continue</size></color></wave>";
        textField.anchoredPosition = new Vector3(100f, 0, 0);

        banner.DOFade(0.8f, bannerFadeTime)
        .SetEase(Ease.OutCirc);

        text.DOFade(1f, textMoveTime / 2);
        textField.DOAnchorPosX(0, textMoveTime)
        .SetEase(Ease.OutCirc)
        .OnComplete(() => {
            storedCallback = callback;
            listeningForSpace = true;
        });
    }

    void CloseMessage(){
        if(listeningForSpace){
            listeningForSpace = false;
            text.DOFade(0f, textMoveTime / 2);

            textField.DOAnchorPosX(-100f, textMoveTime)
            .SetEase(Ease.InCirc)
            .OnComplete(() => {
                storedCallback?.Invoke();
            });

            banner.DOFade(0f, bannerFadeTime)
            .SetEase(Ease.InCirc);
        }
    }
}
