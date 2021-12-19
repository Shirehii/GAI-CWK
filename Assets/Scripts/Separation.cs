using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : SteeringBehaviour
{
    [SerializeField]
    private float neighborhoodDistance = 200;
    [SerializeField]
    private float neighborhoodAngle = 135;

    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        Vector3 repulsiveForce = new Vector3(0, 0, 0);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, neighborhoodDistance);

        List<GameObject> otherAgents = new List<GameObject>(); //list of agents in the neighborhood

        //search for other characters within the specified neighborhood
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Vector3 otherAgentDirection = hitColliders[i].transform.position - transform.position;
            float angle = Vector3.Angle(otherAgentDirection, transform.up);

            if (hitColliders[i].tag == "Follower" && angle <= neighborhoodAngle && -angle >= -neighborhoodAngle)
                otherAgents.Add(hitColliders[i].gameObject);
        }

        //calculate a repulsive force for each other agent
        for (int i = 0; i < otherAgents.Count; i++)
        {
            repulsiveForce += transform.position - otherAgents[i].transform.position;
        }
        repulsiveForce /= otherAgents.Count;
        repulsiveForce = Vector3.Normalize(repulsiveForce) * steeringAgent.MaxSpeed;

        return repulsiveForce;
    }
}
