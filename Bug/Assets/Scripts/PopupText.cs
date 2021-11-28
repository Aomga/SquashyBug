using UnityEngine;
using TMPro;
using DG.Tweening;

public class PopupText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] float moveUpBy = 1;
    [SerializeField] float moveUpDuration = 1;
    [SerializeField] float fadeOutDuration = 0.5f;
    
    public void Setup(string text){
        textMeshPro.text = "<wave>" + text + "</wave>";

        transform.DOMoveY(transform.position.y + moveUpBy, moveUpDuration)
        .SetEase(Ease.OutBack)
        .OnComplete(() => {
            textMeshPro.DOFade(0f, fadeOutDuration).OnComplete(() => {
                Destroy(gameObject);
            });
        });
    }
}
