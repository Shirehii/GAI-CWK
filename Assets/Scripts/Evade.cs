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
            leader = GameObject.FindGameObjectWithTag("Leader"); //TODO: not needed?

        if (targetPosition != Vector3.zero)
        {
            desiredVelocity = Vector3.Normalize(transform.position - targetPosition) * steeringAgent.MaxSpeed;

            steeringVelocity = desiredVelocity - steeringAgent.CurrentVelocity;
        }
        else 
        {
            desiredVelocity = Vector3.zero;
        }

        return desiredVelocity;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Leader"))
        {
            targetPosition = col.gameObject.GetComponent<BoxCollider>().transform.position;
            //print(targetPosition);
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
