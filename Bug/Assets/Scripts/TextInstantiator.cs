using System;
using TMPro;
using UnityEngine;

public class TextInstantiator : MonoBehaviour
{
    [SerializeField] GameObject popupText;

    public static Action<Vector3, string> InstantiateText;

    void Start(){
        InstantiateText += OnInstantiateText;

        //TODO: put this somewhere else
        DG.Tweening.DOTween.defaultAutoKill = true;
    }

    void OnInstantiateText(Vector3 position, string text){
        GameObject newPopupText = Instantiate(popupText, position, Quaternion.identity);
        newPopupText.GetComponent<PopupText>().Setup(text);
    }
}
