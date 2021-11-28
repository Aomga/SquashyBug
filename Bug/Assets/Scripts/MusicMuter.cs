using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using TMPro;

public class MusicMuter : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] float defaultVolume = -6f;
    [SerializeField] RectTransform text;
    [SerializeField] float textPopupHoldDuration;


    private void Start() {
        ButtonListener.MPressed += MuteToggle;
    }

    bool muted = false;
    private void MuteToggle(){
        muted = !muted;

        mixer.SetFloat("masterVol", muted ? -80f : defaultVolume);

        text.GetComponent<TextMeshProUGUI>().text = (muted ? "<dangle>Muted</dangle>" : "<dangle>Unmuted</dangle>");
        AnimateToggleText();
    }

    private void AnimateToggleText(){
        text.DOAnchorPosY(0f, 0.5f)
        .SetEase(Ease.OutBack);

        text.DOAnchorPosY(100f, 0.5f)
        .SetEase(Ease.InBack)
        .SetDelay(textPopupHoldDuration);
    }
}
