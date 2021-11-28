using UnityEngine;
using DG.Tweening;

public class PointJump : MonoBehaviour
{
    public float timeBetweenJumps = 0.5f;
    public float jumpHeight = 1;
    public float jumpDuration = 1;
    public float jumpWindupDuration = 1;
    public int totalJumpsToPoint = 1;
    public GameObject jumpPointParent;
    public int startingPoint = 0;
    [SerializeField] Animator animator;
    public float jumpOffset = 0;
    int currentPoint;
    Transform[] jumpPoints;
    void Start(){
        jumpPoints = jumpPointParent.GetComponentsInChildren<Transform>();

        currentPoint = startingPoint;

        Invoke("JumpToNextPoint", jumpOffset);
    }

    void JumpToNextPoint(){
        currentPoint++;
        if(currentPoint >= jumpPoints.Length){
            currentPoint = 0;
        }
        
        transform.DOLookAt(jumpPoints[currentPoint].transform.position, 0.2f, AxisConstraint.Y);
        animator.Play("Jump-Windup");

        transform.DOJump(jumpPoints[currentPoint].transform.position, jumpHeight, totalJumpsToPoint, jumpDuration)
        .SetEase(Ease.InOutQuint)
        .SetDelay(jumpWindupDuration)
        .OnComplete(() => {
            if(this != null){
                JumpArrived();
            }
        });
    }

    void JumpArrived(){
        Invoke("JumpToNextPoint", timeBetweenJumps);
    }
}
