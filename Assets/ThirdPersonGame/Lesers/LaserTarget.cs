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

    Pool pool;

    void Start()
    {
        pool = FindAnyObjectByType<Pool>();

    }

    public void Damage(float damage) 
    {
        currentHP -= damage;
        HPChanged.Invoke();

        if (currentHP <= 0) 
        {
            pool.PutElementBack(gameObject);
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
