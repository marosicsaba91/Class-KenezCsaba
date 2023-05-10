using UnityEngine;



public class RobotAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSet footStepSet;

    void OnValidate()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float movementSpeed = rb.velocity.magnitude;
        animator.SetFloat("Walk Speed", movementSpeed);


        // TestClass tc1 = new TestClass();
    }

    public void OnFootstep()
    {
        audioSource.clip = footStepSet.GetRandom();
        audioSource.Play();
    }
}
