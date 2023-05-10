using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Camera cam;
    Transform cameraTransform;

    void Start()
    {
        cam = Camera.main;
        cameraTransform = cam.transform;
    }

    void Update()
    {
        Vector3 direction = cameraTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

}
