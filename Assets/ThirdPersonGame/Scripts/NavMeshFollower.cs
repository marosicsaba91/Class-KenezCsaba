using UnityEngine;
using UnityEngine.AI;

public class NavMeshFollower : MonoBehaviour
{
	[SerializeField] Transform target;
	[SerializeField] float targetHeight;
	[SerializeField] NavMeshAgent agent;
	[SerializeField] Transform eyeTransform;
	[SerializeField] float maxDistance = 20;
	[SerializeField] float seeingAngle = 140;

	void OnValidate()
	{
		if (agent != null)
			agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		Vector3 targetHead = target.position + Vector3.up * targetHeight;
		Vector3 eyePosition = eyeTransform.position;

		Vector3 direction = targetHead - eyePosition;
		Vector3 forward = transform.forward;

		float angle = Vector3.Angle(direction, forward);
		if (angle > seeingAngle / 2f)
			return;

		Ray ray = new Ray(eyePosition, direction);
		bool isHit = Physics.Raycast(ray, out RaycastHit hit, maxDistance);

		bool isTargetVisible = isHit && hit.transform == target;

		if(isTargetVisible)
			agent.destination = target.position;
	}

	void OnDrawGizmos()
	{
		if (agent == null)
			return;

		NavMeshPath path = agent.path;

		for (int i = 0; i < path.corners.Length - 1; i++)
		{
			Vector3 pos1 = path.corners[i];
			Vector3 pos2 = path.corners[i + 1];

			Gizmos.color = Color.red;
			Gizmos.DrawLine(pos1, pos2);
		}

	}
}
