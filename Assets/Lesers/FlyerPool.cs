using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class FlyerPool : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("flyer")] Flyer flyerPrefab;
    [SerializeField] int StartInstantiateCount = 10;

    List<Flyer> flyers = new List<Flyer>();

    static FlyerPool instance;   // Singleton p�ld�ny

    int flyerIndex = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < StartInstantiateCount; i++)
            CreateNewElement();
    }

    void CreateNewElement()
    {
        /*
        GameObject protoGO = flyerPrefab.gameObject;
        GameObject newGO = Instantiate(protoGO, transform);
        Flyer newFlyer = newGO.GetComponent<Flyer>();
        */


        Flyer newFlyer = Instantiate(flyerPrefab, transform);





        newFlyer.name = "Flayer " + flyerIndex;
        flyerIndex++;
        flyers.Add(newFlyer);
        Deactivate(newFlyer);
    }

    internal static Flyer GetFlyer()
    {
        if (instance.flyers.Count == 0)
            instance.CreateNewElement();

        Flyer f = instance.flyers[^1];
        instance.flyers.RemoveAt(instance.flyers.Count - 1);
        Activate(f);

        return f;
    }

    internal static void PutFlyerBack(Flyer flyer) 
    {
        Deactivate(flyer);
        instance.flyers.Add(flyer);
    }

    static void Activate(Flyer flyer)
    {
        IResetable[] resetables = flyer.GetComponentsInChildren<IResetable>();
        foreach (IResetable resetable in resetables)
            resetable.Reset();

        flyer.gameObject.SetActive(true);
    }

    static void Deactivate(Flyer flyer)
    {
        flyer.gameObject.SetActive(false);
    }
}
