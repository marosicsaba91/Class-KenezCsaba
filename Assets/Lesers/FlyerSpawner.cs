using UnityEngine;

public class FlyerSpawner : MonoBehaviour
{
    [SerializeField] Bounds area;
    [SerializeField] float spawnTime = 1;
    [SerializeField] GameObject spawnable;

    void Start()
    {
        Invoke(nameof(Generate), spawnTime);
    }

    void Generate() 
    {
        GameObject newObj = Instantiate(spawnable);
        newObj.transform.parent = transform;
        newObj.transform.position = BoundsHelper.GetRandomPoint(area);

        Flyer flyer = newObj.GetComponent<Flyer>();
        flyer.SetArea(area);

        Invoke(nameof(Generate), spawnTime);
    }
}
