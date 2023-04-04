using UnityEngine;

public class FlyerSpawner : MonoBehaviour
{

    [SerializeField] Bounds area;
    [SerializeField] float spawnTime = 1;

    void Start()
    {
        Invoke(nameof(Generate), spawnTime);
    }

    void Generate() 
    {
        Flyer flyer = FlyerPool.GetFlyer();
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
