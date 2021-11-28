using UnityEngine;
using DG.Tweening;

public class KillBug : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Component[] removeOnDeath;

    public bool safeFromKillAll;
    public bool noScore;

    void Start(){
        LevelManager.KillAllBugs += KillWithoutStats;
    }

    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Shoe")){
            Kill();
        }
    }

    void KillWithoutStats(){
        if(!safeFromKillAll){
            Kill(false);
        }
    }
    
    public void Kill(bool trackStats = true){
        RemoveComponents();

        if(trackStats){
            BigData.IncrementTotalBugs();

            if(!noScore){
                LevelManager.BugKilled?.Invoke();
            }
        }

        GetComponent<Splatter>().Splat();

        transform.DOScaleY(0.1f, 0.1f);

        transform.DOScale(Vector3.zero, 5).SetDelay(5).OnComplete(() => {
            Destroy(gameObject);
        });
    }

    private void RemoveComponents(){
        LevelManager.KillAllBugs -= KillWithoutStats;

        foreach (Component toRemove in removeOnDeath)
        {
            Destroy(toRemove);
        }
        Destroy(animator);
    }
}
