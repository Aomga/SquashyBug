using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class FollowMousePosition : MonoBehaviour
{
    [SerializeField] float shoeHoveringHeight = 5;
    [SerializeField] float movementDamping = 0.5f;
    [SerializeField] float maxMoveSpeed = 10;
    [SerializeField] AudioSource stompStartFX;
    [SerializeField] AudioSource stompGroundHitFX;
    public static Action StompStarted;
    public static Action StompEnded;
    public bool stomping = false;

    Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        LevelManager.TotalBugKilledInStomp += DisplayStompScore;
        LevelManager.BugBonusFromStomp += DisplayStompBonus;
    }
    void Update()
    {
        if (stomping == false)
        {
            SetPositionToMousePosition();

            //check to see if we have enough stomps or are in menu
            if(StompNumberText.stomps > 0 || LevelManager.inMenu){
                StartCoroutine(Stomping());
            }
        }

        

    }

    Vector3 currentVelocity;

    //shoots a ray towards the ground
    //sets the shoes position to the collision point with the ground
    private void SetPositionToMousePosition(){
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        LayerMask GroundLayer = LayerMask.NameToLayer("Ground");
        
        if (Physics.Raycast(ray, out hit, 1000f, ~GroundLayer)) {
            Vector3 smoothDampedVector3 = Vector3.SmoothDamp(transform.position, hit.point, ref currentVelocity, movementDamping, maxMoveSpeed);
            transform.position = new Vector3(smoothDampedVector3.x, shoeHoveringHeight, smoothDampedVector3.z);




        }

    }
    //Creates stomp
    IEnumerator Stomping() 
    
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            stomping = true;
            
            StompNumberText.stomps--;
            BigData.IncTotalStomps();

            StompStarted?.Invoke(); //used to calculate how many bugs were killed during a stomp
            stompStartFX.Play(); // swish sound for shoe falling down

            transform.DOMoveY(0,1);

            yield return new WaitForSeconds(1);
            CameraShaker.shakeLight?.Invoke(); //shakes screen - not sure if this should be heavy or light
            stompGroundHitFX.Play(); //stomp sound for shoe hitting ground

            transform.DOMoveY(shoeHoveringHeight, 1);

            if(StompNumberText.stomps <= 0){
                LevelManager.AllStompsUsed?.Invoke();
            }

            StompEnded?.Invoke(); //used to calculate how many bugs were killed during a stomp

            stomping = false;
        }


    }

    void DisplayStompScore(int totalKilled){
        string text = "+ " + totalKilled.ToString();
        TextInstantiator.InstantiateText(transform.position, text);
    }
    void DisplayStompBonus(int totalKilled){
        Vector3 newPos = transform.position + (Vector3.down);
        string text = "<color=#247BA0><size=70%>+" + totalKilled.ToString() + " multi squash!</size></color>";
        TextInstantiator.InstantiateText(newPos,  text);
    }



}
