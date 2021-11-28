using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyBugSpawner : MonoBehaviour
{
    [SerializeField] GameObject jumpyBugPrefab;
    [SerializeField] GameObject jumpPointParent;

    [SerializeField] float jumpDuration = 0.5f;
    [SerializeField] float timeBetweenJumps = 1;

    [SerializeField] int amountToCreate = 3;
    [SerializeField] bool safeFromKillAll = false;
    [SerializeField] bool spawnOnStart = false;
    [SerializeField] bool noScore = false;
    void Start()
    {
        if(spawnOnStart){
            SpawnJumpyBugs();
        }
    }

    public void SpawnJumpyBugs(){
        for (int i = 0; i < amountToCreate; i++)
        {
            GameObject newJumper = Instantiate(jumpyBugPrefab, transform);
            newJumper.GetComponent<PointJump>().jumpPointParent = jumpPointParent;
            newJumper.GetComponent<PointJump>().startingPoint = i;
            newJumper.GetComponent<PointJump>().jumpDuration = jumpDuration;
            newJumper.GetComponent<PointJump>().timeBetweenJumps = timeBetweenJumps;

            if(safeFromKillAll){
                newJumper.GetComponent<KillBug>().safeFromKillAll = true;
            }

            if(noScore){
                newJumper.GetComponent<KillBug>().noScore = true;
            }
        }
    }
}
