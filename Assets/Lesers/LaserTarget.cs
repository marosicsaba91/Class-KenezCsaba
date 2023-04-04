using System;
using System.Collections.Generic;
using UnityEngine;

class LaserTarget : MonoBehaviour, IResetable
{
    [SerializeField] Flyer flyer;
    [SerializeField] float startHP = 100;

    public event Action HPChanged;

    float currentHP;

    public static List<LaserTarget> allTargets = new List<LaserTarget>();

    public float HPRate 
    {
        get => currentHP / startHP;
    }

    /*
    void Start()
    {
        currentHP = startHP;
    }
    */

    public void Damage(float damage) 
    {
        currentHP -= damage;
        HPChanged.Invoke();

        if (currentHP <= 0) 
        {
            FlyerPool.PutFlyerBack(flyer);
        }        
    }

    void OnEnable()
    {
        // Regisztálom
        allTargets.Add(this);
    }

    void OnDisable()
    {
        // Un Regisztrálom
        allTargets.Remove(this);
    }

    public void Reset()
    {
        currentHP = startHP;
    }
}
