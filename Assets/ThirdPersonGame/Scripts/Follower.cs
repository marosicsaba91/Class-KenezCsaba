using UnityEngine;

public abstract class Follower : MonoBehaviour
{
	[SerializeField] protected Transform target;
	[SerializeField] Rigidbody rigidBody;

	void OnValidate()
	{
		if(rigidBody == null)
			rigidBody = GetComponent<Rigidbody>();
	}

	protected abstract Vector3 GetVelocity();

	void FixedUpdate()
	{
		rigidBody.velocity = GetVelocity();
	}
	
	protected Vector3 TargetDirection() 
	{
		Vector3 direction = target.position - transform.position;
		direction.Normalize();
		return direction;
	}
}
