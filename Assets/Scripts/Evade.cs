using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : SteeringBehaviour
{
    [SerializeField]
    private GameObject leader;
    Vector3 targetPosition;

    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        if (leader == null)
            leader = GameObject.FindGameObjectWithTag("Leader");

        if (targetPosition != Vector3.zero) //if there is a target position...
        {
            //...flee
            desiredVelocity = Vector3.Normalize(transform.position - targetPosition) * steeringAgent.MaxSpeed;

            desiredVelocity = desiredVelocity - steeringAgent.CurrentVelocity;
        }
        else 
        {
            //else, return a zero vector
            desiredVelocity = Vector3.zero;
        }

        return desiredVelocity;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Leader"))
        {
            targetPosition = col.gameObject.GetComponent<BoxCollider>().transform.position;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Leader"))
        {
            targetPosition = Vector3.zero;
        }
    }
}
