using UnityEngine;

public class AcceleratingFollower : Follower
{
	[SerializeField] float acceleration;
	[SerializeField] float maxSpeed;

	Vector3 velocity;

	protected override Vector3 GetVelocity() 
	{
		velocity += acceleration * TargetDirection() * Time.fixedDeltaTime;

		if(velocity.magnitude > maxSpeed)
			velocity = velocity.normalized * maxSpeed;

		return velocity;
	}
}
