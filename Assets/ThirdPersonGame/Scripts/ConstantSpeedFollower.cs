using UnityEngine;

public class ConstantSpeedFollower : Follower
{
	[SerializeField] float constantSpeed;

	protected override Vector3 GetVelocity()
	{
		return TargetDirection() * constantSpeed;
	}

}
