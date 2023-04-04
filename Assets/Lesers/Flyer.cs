using UnityEngine;

class Flyer : MonoBehaviour
{
    [SerializeField] Bounds area;
    [SerializeField] float speed = 3;
    [SerializeField] float angularSpeed = 180;

    Vector3 targetPoint;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPoint, 0.1f);
    }

    void Start()
    {
        targetPoint = BoundsHelper.GetRandomPoint(area);
    }

    void Update()
    {
        Vector3 selfPoint = transform.position;
        Vector3 targetDirection = targetPoint - selfPoint;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        float roationStep = angularSpeed * Time.deltaTime;
        transform.rotation = 
            Quaternion.RotateTowards(transform.rotation, targetRotation, roationStep);

        float step = speed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, targetPoint);
        if (distance <= step)
            targetPoint = BoundsHelper.GetRandomPoint(area);

        transform.position += transform.forward * step;
    }

    public void SetArea(Bounds area)
    {
        this.area = area;
    }
}
