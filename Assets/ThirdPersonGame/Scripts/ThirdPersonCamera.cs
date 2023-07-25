using UnityEngine;

[ExecuteAlways]
public class ThirdPersonCamera : MonoBehaviour, IResetable
{
    [Header("Referenecs")]
    public Transform target;
    [SerializeField] Transform distanceObject;
    
    [SerializeField] new Camera camera;

    [Header("Settings")]
    [SerializeField] float verticalRotaion = 0;
    [SerializeField] float verticalAngleMin = 10;
    [SerializeField] float verticalAngleMax = 90;

    [SerializeField] float horizontalRotaion = 0;
    [SerializeField] float fieldOfView = 30;
    [SerializeField] float targetSize = 10;

    [SerializeField] float horizontalSensitivity = 2;
    [SerializeField] float verticalSensitivity = 2;

    public void Reset()
    {
        Debug.Log("Reset");
    }

    void Update()
    {
        if (target == null) return;

        float xMovement = Input.GetAxis("Mouse X") * horizontalSensitivity;
        float yMovement = Input.GetAxis("Mouse Y") * verticalSensitivity;

        horizontalRotaion += xMovement;
        verticalRotaion -= yMovement;

        verticalRotaion = Mathf.Clamp(verticalRotaion, verticalAngleMin, verticalAngleMax);


        // ----------------------------


        float tg = Mathf.Tan(fieldOfView / 2f * Mathf.Deg2Rad);

        float distance = targetSize / (2f * tg);

        transform.SetPositionAndRotation(
            target.position,
            Quaternion.Euler(verticalRotaion, horizontalRotaion, 0));

        distanceObject.localPosition = new Vector3(0, 0, -distance);

        Vector3 forward = target.position - camera.transform.position;
        camera.transform.rotation = Quaternion.LookRotation(forward);

        camera.fieldOfView = fieldOfView;
    }
}
