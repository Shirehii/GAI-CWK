using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alignment : SteeringBehaviour
{
    [SerializeField]
    private int detectionRadius = 500;
    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        // Alignment steering behavior gives an character the ability to align itself with (that is, head in
        //the same direction and/ or speed as) other nearby characters, as shown in Figure 16.Steering
        //for alignment can be computed by finding all characters in the local neighborhood(as
        //described above for separation)
        Vector3 alignmentForce = new Vector3(0, 0, 0);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        List<GameObject> otherAgents = new List<GameObject>();

        //search for other characters within the specified neighborhood
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Follower")
                otherAgents.Add(hitColliders[i].gameObject);

        }

        //averaging together the velocity(or alternately, the unit forward vector) of the nearby characters
        Vector3 averageForce = new Vector3(0, 0, 0);
        for (int i = 0; i < otherAgents.Count; i++)
        {
            averageForce += otherAgents[i].transform.forward;
        }
        averageForce = averageForce / otherAgents.Count;


        //This average is the “desired velocity,” and so the steering vector is the difference between the average
        //and our character’s current velocity(or alternately, its unit forward vector).This steering will tend to
        //turn our character so it is aligned with its neighbors.
        alignmentForce = averageForce - transform.forward;

        return alignmentForce;
    }
}
