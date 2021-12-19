using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFollowing : SteeringBehaviour
{
    /// <summary>
    /// Controls how far from the target position should the agent start to slow down
    /// NOTE: [SerializeField] exposes a C# variable to Unity's inspector without making it public. Useful for encapsulating code
    /// while still giving access to the Unity inspector
    /// </summary>
    [SerializeField]
    protected float arrivalRadius = 200.0f;

    [SerializeField]
    private GameObject leader;

    [SerializeField]
    private float offset = 100;

    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        if (leader == null)
            leader = GameObject.FindGameObjectWithTag("Leader");

        //get the target position, a point offset slightly behind the leader
        Vector3 targetPosition = leader.transform.position - (leader.transform.up * offset);

        //seek
        desiredVelocity = Vector3.Normalize(targetPosition - transform.position);

        //arrival
        float distance = (targetPosition - transform.position).magnitude;
        if (distance < arrivalRadius)
        {
            desiredVelocity *= steeringAgent.MaxSpeed * (distance / arrivalRadius);
        }
        else
        {
            desiredVelocity *= steeringAgent.MaxSpeed;
        }

        steeringVelocity = desiredVelocity - steeringAgent.CurrentVelocity;
        return steeringVelocity;
    }
}
