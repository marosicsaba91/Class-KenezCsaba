using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
class ThirdPersonShooter : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] float shootSpeed = 10;
    [SerializeField] float ballisticPathDuration = 3;
    [SerializeField] KeyCode shootKey = KeyCode.Space;
    [SerializeField] GameObject bullet;
    [SerializeField] LayerMask raycastMask;

    Pool pool;

    void OnValidate()
    {
        if(lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        pool = FindAnyObjectByType<Pool>();
    }

    void Update()
    {
        DrawBallisticPath();

        if (Application.isPlaying) 
        {
            HandleSoting();
        }
    }

    void HandleSoting()
    {
        bool shoot = Input.GetKeyDown(shootKey);

        if (!shoot) return;

        GameObject newBullet = pool.GetElement();
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * shootSpeed;
        rb.position = transform.position;

    }

    void DrawBallisticPath()
    { 
        Vector3 startPosition = transform.position;
        Vector3 direction = transform.forward;
        Vector3 gravity = Physics.gravity;
        float tick = Time.fixedDeltaTime;

        List<Vector3> list = CalculateBallisticPath(true);


        Vector3[] array = list.ToArray();
        lineRenderer.positionCount = array.Length;
        lineRenderer.SetPositions(array);
    }

    List<Vector3> CalculateBallisticPath(bool doRaycast)
    { 
        Vector3 direction = transform.forward;
        Vector3 gravity = Physics.gravity;
        float deltaTime = Time.fixedDeltaTime;
        Vector3 position = transform.position;
        Vector3 velocity = direction * shootSpeed;
        float drag = bullet.GetComponent<Rigidbody>().drag;

        List<Vector3> path = new List<Vector3>();
        path.Add(position);
        Vector3 lastPoint = position;

        for (float t = 0; t <= ballisticPathDuration; t += deltaTime)
        {
            velocity += gravity * deltaTime;
            velocity *= 1f - (drag * deltaTime);
            position += velocity * deltaTime;

            if (doRaycast) 
            {
                Vector3 step = position - lastPoint;
                Ray ray = new Ray(lastPoint, step); 
                if (Physics.Raycast(ray, out RaycastHit hit, step.magnitude, raycastMask)) 
                {
                    path.Add(hit.point);
                    return path;
                }
            }

            path.Add(position);
            lastPoint = position;
        }

        return path;
    }
}
