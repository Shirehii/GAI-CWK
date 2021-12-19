using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : SteeringBehaviour
{
    [SerializeField]
    private float neighborhoodDistance = 500;
    [SerializeField]
    private float neighborhoodAngle = 135;

    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        Vector3 attractiveForce;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, neighborhoodDistance);
        
        List<GameObject> otherAgents = new List<GameObject>();

        //search for other characters within the specified neighborhood
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Vector3 otherAgentDirection = hitColliders[i].transform.position - transform.position;
            float angle = Vector3.Angle(otherAgentDirection, transform.up);

            if (hitColliders[i].tag == "Follower" && angle <= neighborhoodAngle && -angle >= -neighborhoodAngle)
                otherAgents.Add(hitColliders[i].gameObject);
        }

        //computing the “average position” (or “center of gravity”) of the nearby characters.
        Vector3 averagePosition = new Vector3(0, 0, 0);
        for (int i = 0; i < otherAgents.Count; i++)
        {
            averagePosition += otherAgents[i].transform.position;
        }
        averagePosition /= otherAgents.Count;

        attractiveForce = Vector3.Normalize(averagePosition - transform.position) * steeringAgent.MaxSpeed;

        return attractiveForce;
    }
}
