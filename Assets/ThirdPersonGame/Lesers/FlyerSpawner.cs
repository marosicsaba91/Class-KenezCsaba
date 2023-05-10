using UnityEngine;

public class FlyerSpawner : MonoBehaviour
{

    [SerializeField] Bounds area;
    [SerializeField] float spawnTime = 1;

    Pool pool;

    void Start()
    {
        pool = FindAnyObjectByType<Pool>();
        Invoke(nameof(Generate), spawnTime);
    }

    void Generate() 
    {
        GameObject go = pool.GetElement();
        Flyer flyer = go.GetComponent<Flyer>();
        flyer.transform.position = BoundsHelper.GetRandomPoint(area);         
        flyer.SetArea(area);

        Invoke(nameof(Generate), spawnTime);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(area.center, area.size);
    }
}
