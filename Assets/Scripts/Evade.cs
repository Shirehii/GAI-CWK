using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : SteeringBehaviour
{
    [SerializeField]
    private GameObject leader;

    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        if (leader == null)
            leader = GameObject.FindGameObjectWithTag("Leader");

        

        return desiredVelocity;
    }
}
