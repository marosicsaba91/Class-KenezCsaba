using UnityEngine;

public class DistanceBasedFollower : Follower
{
	[SerializeField] AnimationCurve speedOverDistance;

	protected override Vector3 GetVelocity()
	{
		float distance = Vector3.Distance(transform.position, target.position);
		float speed = speedOverDistance.Evaluate(distance); 

		return TargetDirection() * speed;
	}
}
