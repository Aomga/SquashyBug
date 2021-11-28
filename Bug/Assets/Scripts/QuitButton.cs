using UnityEngine;
using DG.Tweening;

public class QuitButton : MonoBehaviour
{
    [SerializeField] AudioSource quitButtonFX;
    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Shoe")){
            quitButtonFX.Play();
            transform.DOMoveY(-0.2f, 0.1f).OnComplete(() => {
                transform.DOMoveY(0, 5f).SetDelay(2f);
            });
            Invoke("QuitGame", 1);
        }
    }

    void QuitGame(){
        Application.Quit();
    }
}
