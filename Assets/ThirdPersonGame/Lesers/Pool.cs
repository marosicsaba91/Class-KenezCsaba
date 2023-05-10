using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] GameObject prototypePrefab;
    [SerializeField] int StartInstantiateCount = 10;

    List<GameObject> gameObjects = new List<GameObject>();

    int poolIndex = 0;


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


        GameObject newGO = Instantiate(prototypePrefab, transform); 


        newGO.name = prototypePrefab.name + poolIndex;
        poolIndex++;
        gameObjects.Add(newGO);
        Deactivate(newGO);
    }

    internal GameObject GetElement()
    {
        if (gameObjects.Count == 0)
            CreateNewElement();

        GameObject go = gameObjects[^1];
        gameObjects.RemoveAt(gameObjects.Count - 1);
        Activate(go);

        return go;
    }

    internal void PutElementBack(GameObject element) 
    {
        Deactivate(element);
        gameObjects.Add(element);
    }

    static void Activate(GameObject element)
    {
        IResetable[] resetables = element.GetComponentsInChildren<IResetable>();
        foreach (IResetable resetable in resetables)
            resetable.Reset();

        element.SetActive(true);
    }

    static void Deactivate(GameObject element)
    {
        element.SetActive(false);
    }
}
