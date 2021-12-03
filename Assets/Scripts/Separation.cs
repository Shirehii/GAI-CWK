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

            if (hitColliders[i].tag == "Follower" && angle <= neighborhoodAngle && -angle >= -neighborhoodAngle) //TODO: determine if -angle >= -neighborhoodAngle is needed
                otherAgents.Add(hitColliders[i].gameObject);
        }

        //for each nearby character -> a repulsive force is computed by subtracting the positions of our character and the nearby
        //character, normalizing, and then applying a 1 / r weighting. (That is, the position offset vector is
        //scaled by 1 / r^2.) Note that 1 / r is just a setting that has worked well, not a fundamental value.
        //These repulsive forces for each nearby character are summed together to produce the overall steering force.
        for (int i = 0; i < otherAgents.Count; i++)
        {
            repulsiveForce += transform.position - otherAgents[i].transform.position;
        }
        repulsiveForce /= otherAgents.Count; //TODO: determine if this is needed
        repulsiveForce = Vector3.Normalize(repulsiveForce) * steeringAgent.MaxSpeed;

        //TODO: return steeringVelocity instead?
        return repulsiveForce;
    }
}
