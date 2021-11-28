using UnityEngine;
using DG.Tweening;

public class PlayButton : MonoBehaviour
{
    [SerializeField] AudioSource playButtonFX;

    private bool startingGame = false;
    void OnTriggerEnter(Collider collider){
        if(!startingGame){
            if(collider.CompareTag("Shoe")){
                startingGame = true;
                playButtonFX.Play();
                transform.DOMoveY(-0.2f, 0.1f).OnComplete(() => {
                    transform.DOMoveY(0, 5f).SetDelay(2f);
                });
                Invoke("StartGame", 1);
            }
        }
    }

    void StartGame(){
            LevelManager.StartGame?.Invoke();

            BigData.ClearGameTotals();
            BigData.IncTotalGamesPlayed();
    }
}
