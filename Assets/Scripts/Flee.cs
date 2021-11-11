using UnityEngine;

public class Flee : SteeringBehaviour
{
	public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
	{
		// Get the target position from the mouse input
		Vector3 targetPosition = Helper.GetMousePosition();

		// Get the desired velocity for seek and limit to maxSpeed
		desiredVelocity = Vector3.Normalize(transform.position - targetPosition) * steeringAgent.MaxSpeed;

		// Calculate steering velocity
		steeringVelocity = desiredVelocity - steeringAgent.CurrentVelocity;
		return steeringVelocity;
	}
}
