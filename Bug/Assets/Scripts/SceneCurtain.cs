using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class SceneCurtain : MonoBehaviour
{
    [SerializeField] Image curtain;
    void Start()
    {
        curtain.enabled = true;
        curtain.DOFade(0f, 0.2f);
    }

}
