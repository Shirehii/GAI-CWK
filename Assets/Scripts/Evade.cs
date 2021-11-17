using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : SteeringBehaviour
{
    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        return desiredVelocity;
    }
}
