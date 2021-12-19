using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alignment : SteeringBehaviour
{
    [SerializeField]
    private float neighborhoodDistance = 500;
    [SerializeField]
    private float neighborhoodAngle = 135;

    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        Vector3 alignmentForce = new Vector3(0, 0, 0);

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

        //averaging together the velocity(or alternately, the unit forward vector) of the nearby characters
        Vector3 averageForce = new Vector3(0, 0, 0);
        for (int i = 0; i < otherAgents.Count; i++)
        {
            averageForce += otherAgents[i].transform.up;
        }
        averageForce /= otherAgents.Count;

        alignmentForce = Vector3.Normalize(averageForce - transform.up) * steeringAgent.MaxSpeed;

        return alignmentForce;
    }
}
