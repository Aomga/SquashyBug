using System;
using UnityEngine;
using DG.Tweening;

public class Stomp : MonoBehaviour
{
    [SerializeField] AudioSource stompStartFX;
    [SerializeField] AudioSource stompGroundHitFX;
    // [SerializeField] ParticleSystem stompParticles;
    public static Action StompStarted;
    public static Action StompEnded;
    public static bool stomping = false;

    public static int stomps = 0;

    private void Start() {
        LevelManager.TotalStomps += SetStomps;
    }

    void SetStomps(int levelStomps){
        stomps = levelStomps;
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !stomping){
            if(stomps > 0 || LevelManager.inMenu){
                DoStomp();
            }
        }
    }

     void DoStomp(){
        stomping = true;
        stompStartFX.Play();
        
        //in the menu, dont count stomps
        if(!LevelManager.inMenu){
            stomps--;
            StompNumberText.stomps--;
        }

        BigData.IncTotalStomps();
        StompStarted?.Invoke();

        transform.DOMoveY(0.1f,0.3f).SetEase(Ease.InBack)
        .OnComplete(() => {
            CameraShaker.shakeHeavy?.Invoke();
            stompGroundHitFX.Play();
            // stompParticles.Play();

            transform.DOPunchScale(Vector3.one * 0.1f, 0.1f).OnComplete(() => {
                
                if(stomps <= 0){
                    LevelManager.AllStompsUsed?.Invoke();
                }

                StompEnded?.Invoke();

                stomping = false;
            });
        });
    }
}
