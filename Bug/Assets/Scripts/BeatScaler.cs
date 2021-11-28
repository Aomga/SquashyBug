using UnityEngine;
using DG.Tweening;

public class BeatScaler : MonoBehaviour
{
    [SerializeField] private float scaleAmount;
    [SerializeField] private float duration;

    private Vector3 originalScale;
    
    void Awake(){
        BeatEmitter.Beat += PulseScale;
    }

    void Start(){
        originalScale = transform.localScale;
    }

    void PulseScale(){
        transform.localScale = originalScale + Vector3.one * scaleAmount;
        transform.DOScale(originalScale, duration).SetEase(Ease.OutCubic);
    }

    void OnDestroy(){
        BeatEmitter.Beat -= PulseScale;
    }
}
