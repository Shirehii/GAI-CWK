using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : SteeringBehaviour
{
    [SerializeField]
    private int detectionRadius = 200;

    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        Vector3 repulsiveForce = new Vector3(0, 0, 0);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        List<GameObject> otherAgents = new List<GameObject>();

        //search for other characters within the specified neighborhood
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Follower")
                otherAgents.Add(hitColliders[i].gameObject);
            
        }

        //for each nearby character -> a repulsive force is computed by subtracting the positions of our character and the nearby
        //character, normalizing, and then applying a 1 / r weighting. (That is, the position offset vector is
        //scaled by 1 / r^2.) Note that 1 / r is just a setting that has worked well, not a fundamental value.
        //These repulsive forces for each nearby character are summed together to produce the overall steering force.
        for (int i = 0; i < otherAgents.Count; i++)
        {
            repulsiveForce += (Vector3.Normalize(transform.position - otherAgents[i].transform.position) * steeringAgent.MaxSpeed);
        }
        
        return repulsiveForce;
    }
}
