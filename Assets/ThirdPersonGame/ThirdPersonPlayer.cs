using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 5.0f;

    [SerializeField] Transform cameraTransform;
    [SerializeField] float maxAngularSpeed;


    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        Vector3 inputDir = forward * z + right * x;
        inputDir.y = 0;
        inputDir.Normalize();

        rb.velocity = inputDir * speed;

        if (inputDir != Vector3.zero) 
        {
            Quaternion targetRotaion = Quaternion.LookRotation(inputDir);

            float angle = maxAngularSpeed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotaion, angle);
        }

    }
}
