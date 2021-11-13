using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : SteeringBehaviour
{
    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        // Cohesion steering behavior gives an character the ability to cohere with(approach and form a
        //group with) other nearby characters.See Figure 15. Steering for cohesion can be computed by
        //finding all characters in the local neighborhood(as described above for separation),

        int detectionRadius = 500;
        Vector3 attractiveForce = new Vector3(0, 0, 0);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        List<GameObject> otherAgents = new List<GameObject>();

        //search for other characters within the specified neighborhood
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Follower")
                otherAgents.Add(hitColliders[i].gameObject);

        }

        //computing the “average position” (or “center of gravity”) of the nearby characters.
        Vector3 averagePosition = new Vector3(0, 0, 0);
        for (int i = 0; i < otherAgents.Count; i++)
        {
            averagePosition += otherAgents[i].transform.position;
        }
        averagePosition = averagePosition / otherAgents.Count;

        //The steering force can applied in the direction of that “average position” (subtracting our character position
        //from the average position, as in the original boids model), or it can be used as the target for
        //seek steering behavior.
        attractiveForce = averagePosition - transform.position;

        return attractiveForce;
    }
}
