using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float walkMaxSpeed = 2.0f;
    [SerializeField] float runMaxSpeed = 6.0f;
    [SerializeField] float acceleration = 3.0f;
    [SerializeField] float deceleration = 5.0f;

    [SerializeField] Transform cameraTransform;
    [SerializeField] float maxAngularSpeed;


    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool run = Input.GetAxis("Run") != 0;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        Vector3 inputDir = forward * z + right * x;
        inputDir.y = 0;
        inputDir.Normalize();

        Vector3 velocity = rb.velocity;
         

        if (inputDir == Vector3.zero) // LASSULÁS
        {
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }
        else // GYORSULÁS
        { 
            float maxSpeed = run ? runMaxSpeed : walkMaxSpeed;
            velocity = Vector3.MoveTowards(velocity, inputDir * maxSpeed, acceleration * Time.fixedDeltaTime); 
        }

        rb.velocity = velocity;

        if (inputDir != Vector3.zero) 
        {
            Quaternion targetRotaion = Quaternion.LookRotation(inputDir);

            float angle = maxAngularSpeed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotaion, angle);
        }

    }
}
