using System;
using System.Collections.Generic;
using UnityEngine;

class Laser : MonoBehaviour
{
    [SerializeField] float range = 10;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float damage = 50;

    void OnValidate()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);

        if (currentTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(currentTarget.transform.position, transform.position);
        }
    }

    LaserTarget currentTarget = null;

    void Update()
    {
        FindTarget();
        ShootTarget();
    }

    void ShootTarget()
    {
        if (currentTarget != null) 
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentTarget.transform.position);

            currentTarget.Damage(damage * Time.deltaTime);
        }
        
        lineRenderer.enabled = currentTarget != null;

    }

    private void FindTarget()
    {
        List<LaserTarget> targets = LaserTarget.allTargets;

        if (targets.Count == 0)
        {
            currentTarget = null;
            return;
        }

        Vector3 selfPos = transform.position;
        LaserTarget closest = targets[0];
        float closestDistance = Vector3.Distance(closest.transform.position, selfPos);

        for (int i = 1; i < targets.Count; i++)
        {
            LaserTarget target = targets[i];
            float distance = Vector3.Distance(target.transform.position, selfPos);

            if (distance < closestDistance)
            {
                closest = target;
                closestDistance = distance;
            }
        }

        // Closest
        if (closestDistance <= range)
        {
            currentTarget = closest;
        }
        else
        {
            currentTarget = null;
        }
    }
}
