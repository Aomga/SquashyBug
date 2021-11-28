using PathCreation;
using PathCreation.Examples;
using UnityEngine;

[RequireComponent(typeof(PathCreator))]
public class CreatePathFollowers : MonoBehaviour
{
    [SerializeField] GameObject followerPrefab;
    [SerializeField] float followersSpeed = 2;
    [SerializeField] int amountToCreate = 0;
    [SerializeField] int spacing = 3;

    [SerializeField] float minRandomScale = 0.3f, maxRandomScale = 1.5f; 
    [SerializeField] float spacingRandomDown = 0.5f, spacingRandomUp = 0.5f; 

    [SerializeField] bool fillPath;
    [SerializeField] bool randomizeSpacing;
    [SerializeField] bool spawnOnStart = false;
    [SerializeField] bool safeFromKillAll = false;
    [SerializeField] bool noScore = false;
    void Start()
    {
        if(spawnOnStart){
            SpawnPathFollowers();
        }
    }

    public void SpawnPathFollowers(){
        if(fillPath){
            amountToCreate = (int)GetComponent<PathCreator>().path.length / spacing;
        }

        for (int i = 0; i < amountToCreate; i++)
        {
            float randomizedSpacing = Random.Range(spacing - spacingRandomDown, spacing + spacingRandomUp);
            GameObject newFollowerPrefab = Instantiate(followerPrefab, transform);
            newFollowerPrefab.transform.localScale = Vector3.one * Random.Range(minRandomScale, maxRandomScale);
            newFollowerPrefab.GetComponent<PathFollower>().pathCreator = GetComponent<PathCreator>();
            newFollowerPrefab.GetComponent<PathFollower>().speed = followersSpeed;
            newFollowerPrefab.GetComponentInChildren<Animator>().speed = followersSpeed / 2;
            newFollowerPrefab.GetComponent<PathFollower>().offset = randomizeSpacing ? i * randomizedSpacing : i * spacing;

            if(safeFromKillAll){
                newFollowerPrefab.GetComponent<KillBug>().safeFromKillAll = true;
            }

            if(noScore){
                newFollowerPrefab.GetComponent<KillBug>().noScore = true;
            }
        }
    }

}
