using UnityEngine;

public class AngleTextUpwards : MonoBehaviour
{
    //not sure why but the text keeps reseting the rotation
    //this script points the text up 45 degress towards the camera
    void Start()
    {
        transform.Rotate(new Vector3(45f, 0f, 0f));       
    }
}
