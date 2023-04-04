using UnityEngine;

[ExecuteAlways]
public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Referenecs")]
    [SerializeField] Transform target;
    [SerializeField] Transform horizontalDistanceObject;
    [SerializeField] Transform verticalDistanceObject;
    
    [SerializeField] new Camera camera;

    [Header("Settings")]
    [SerializeField] float horizontalDistane = 10;
    [SerializeField] float verticalDistane = 5;
    [SerializeField] float horizontalRotaion = 0;

    void Update()
    {
        transform.SetPositionAndRotation(
            target.position,
            Quaternion.Euler(0, horizontalRotaion, 0));

        horizontalDistanceObject.localPosition = new Vector3(0, 0, -horizontalDistane);
        verticalDistanceObject.localPosition = new Vector3(0, verticalDistane, 0);

        Vector3 forward = target.position - camera.transform.position;
        camera.transform.rotation = Quaternion.LookRotation(forward);
    }
}
