using System.Collections;
using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class ParentPathFollower : PathFollower
{
    // Start is called before the first frame update
    void Start()
    {
        pathCreator = GetComponentInParent<PathCreator>();
    }
}
