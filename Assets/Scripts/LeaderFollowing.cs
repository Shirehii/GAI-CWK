using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFollowing : SteeringBehaviour
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

    [SerializeField]
    private float offset = 100;

    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent) //TODO: make followers faster the farther away they are from the leader
    {
        //Leader following behavior causes one or more character to follow another moving character designated as the leader.Generally the followers want to stay near the leader, without
        //crowding the leader, and taking care to stay out of the leader’s way(in case they happen to find them selves in front of the leader). In addition, if there is more than one follower, they
        //want to avoid bumping each other. The implementation of leader following relies on arrival behavior(see above) a desire to move towards a point, slowing as it draws near. The arrival
        //target is a point offset slightly behind the leader. (The offset distance might optionally increases with speed.) If a follower finds itself in a rectangular region in front of the leader, it
        //will steer laterally away from the leader’s path before resuming arrival behavior. In addition the followers use separation behavior to prevent crowding each other

        if (leader == null)
            leader = GameObject.FindGameObjectWithTag("Leader");

        //get the target position, a point offset slightly behind the leader (The offset distance might optionally increases with speed.)
        Vector3 targetPosition = leader.transform.position - (leader.transform.up * offset);

        // Get the desired unit velocity for seek - we'll apply the maxSpeed later depending on where the agent is
        desiredVelocity = Vector3.Normalize(targetPosition - transform.position);

        // Check to see if agent is in the circle radius and if so decrease speed in a linear way, otherwise just
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
