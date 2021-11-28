using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatter : MonoBehaviour
{
    [SerializeField] GameObject splatPrefab;
    public void Splat(){
        SplatFX.PlayRandomSplat.Invoke();

        //always add one splat directly on bug
        Instantiate(splatPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);

        //randomly spawn extra splats
        for (int i = 0; i < Random.Range(0,4); i++)
        {
            Vector3 nearbyPositions = new Vector3(transform.position.x + Random.Range(-1, 1f), 0 , transform.position.z + Random.Range(-1, 1f));
            Instantiate(splatPrefab, nearbyPositions, Quaternion.identity);
        }
    }
}
