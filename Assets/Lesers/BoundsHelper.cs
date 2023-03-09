using UnityEngine;

static class BoundsHelper
{
    public static Vector3 GetRandomPoint(Bounds area)
    {
        float x = Random.Range(area.min.x, area.max.x);
        float y = Random.Range(area.min.y, area.max.y);
        float z = Random.Range(area.min.z, area.max.z);

        return new Vector3(x, y, z);
    }
}
