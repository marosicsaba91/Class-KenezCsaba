using UnityEngine;

public class Bullet : MonoBehaviour
{
    Pool pool;

    void Start()
    {
        pool = FindAnyObjectByType<Pool>();
    }

    void OnCollisionEnter(Collision collision)
    {
        pool.PutElementBack(gameObject);
    }

}
