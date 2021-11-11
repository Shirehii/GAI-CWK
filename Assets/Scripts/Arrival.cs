using UnityEngine;

public class Arrival : SteeringBehaviour
{
    /// <summary>
    /// Controls how far from the target position should the agent start to slow down
    /// NOTE: [SerializeField] exposes a C# variable to Unity's inspector without making it public. Useful for encapsulating code
    /// while still giving access to the Unity inspector
    /// </summary>
    [SerializeField]
    protected float arrivalRadius = 200.0f;

    [SerializeField]
    private GameObject leader;

    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        // Get the target position from the leader. If it doesn't exist, get the moust position instead
        Vector3 targetPosition = new Vector3(0, 0, 0);
        if (leader != null)
        {
            targetPosition = leader.transform.position;
        }
        else
        {
            targetPosition = Helper.GetMousePosition();
        }

        // Get the desired unit velocity for seek - we'll apply the maxSpeed later depending on where the agent is
        desiredVelocity = Vector3.Normalize(targetPosition - transform.position);

        // Check to see if agent is in the circle radius and is so decreasespeed in a linear way, otherwise just
        // do the last step of seek which is apply max speed
        float distance = (targetPosition - transform.position).magnitude;
        if (distance < arrivalRadius)
        {
            desiredVelocity *= steeringAgent.MaxSpeed * (distance / arrivalRadius);
        }
        else
        {
            desiredVelocity *= steeringAgent.MaxSpeed;
        }

        // Calculate steering velocity
        steeringVelocity = desiredVelocity - steeringAgent.CurrentVelocity;
        return steeringVelocity;
    }
}
