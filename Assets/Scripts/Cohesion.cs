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
        // Cohesion steering behavior gives an character the ability to cohere with(approach and form a
        //group with) other nearby characters.See Figure 15. Steering for cohesion can be computed by
        //finding all characters in the local neighborhood(as described above for separation),
        Vector3 attractiveForce;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, neighborhoodDistance);
        
        List<GameObject> otherAgents = new List<GameObject>();

        //search for other characters within the specified neighborhood
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Vector3 otherAgentDirection = hitColliders[i].transform.position - transform.position;
            float angle = Vector3.Angle(otherAgentDirection, transform.up);

            if (hitColliders[i].tag == "Follower" && angle <= neighborhoodAngle && -angle >= -neighborhoodAngle) //TODO: determine if -angle >= -neighborhoodAngle is needed
                otherAgents.Add(hitColliders[i].gameObject);
        }

        //computing the “average position” (or “center of gravity”) of the nearby characters.
        Vector3 averagePosition = new Vector3(0, 0, 0);
        for (int i = 0; i < otherAgents.Count; i++)
        {
            averagePosition += otherAgents[i].transform.position;
        }
        averagePosition /= otherAgents.Count;

        //The steering force can applied in the direction of that “average position” (subtracting our character position
        //from the average position, as in the original boids model), or it can be used as the target for
        //seek steering behavior.
        attractiveForce = (averagePosition - transform.position) * steeringAgent.MaxSpeed;

        return attractiveForce;
    }
}
