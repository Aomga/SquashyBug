using System.Collections;
using UnityEngine;

public class Rotatetor : MonoBehaviour
{
    public bool canRotate = true;
    [SerializeField]
    float speed = 30; 
    void Update()
    {
        StartCoroutine(Movement());
    }


    IEnumerator Movement()
    
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            canRotate = false;
            yield return new WaitForSeconds(2);
            canRotate = true;
            
        }
        if (canRotate == true)    
        {

            if (Input.GetKey(KeyCode.A))
                transform.Rotate(Vector3.up * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.D))
                transform.Rotate(-Vector3.up * speed * Time.deltaTime);

        }


    }

}

