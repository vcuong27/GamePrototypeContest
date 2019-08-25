using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectsPool<T>
{
    private static Dictionary<string, List<GameObject>> pool = new Dictionary<string, List<GameObject>>();

    private const string instancesOptionalNameEnding = "(Pool)";
    
    /// <summary>
    /// Returns an instance of the <paramref name="prefab"/>. The returned gameobject is active.
    /// </summary>
    public static T GetInstance(GameObject prefab)
    {
        GameObject prefabInstance = GetInstanceFromPool(prefab);
        if (prefabInstance == null)
        {
            prefabInstance = Instantiate(prefab);
            StoreInstance(prefabInstance);
        }

        GameObject gameObjectInstance = prefabInstance.gameObject;
        gameObjectInstance.SetActive(true);


        return gameObjectInstance.GetComponent<T>();
    }

    /// <summary>
    /// Clears the pool.
    /// </summary>
    public static void Clear()
    {
        pool.Clear();
    }

    /// <summary>
    /// Deactivates <paramref name="gameObjectInstance"/> and adds it to the pool.
    /// </summary>
    private static void StoreInstance(GameObject gameObjectInstance)
    {
        gameObjectInstance.gameObject.SetActive(false);
        List<GameObject> instancesList;
        if (pool.TryGetValue(gameObjectInstance.name, out instancesList))
        {
            instancesList.Add(gameObjectInstance);
        }
        else
        {
            instancesList = new List<GameObject>();
            instancesList.Add(gameObjectInstance);
            pool.Add(gameObjectInstance.name, instancesList);
        }
    }


    /// <summary>
    /// Returns an instance of the <paramref name="prefab"/> from the pool if any, null otherwise.
    /// </summary>
    private static GameObject GetInstanceFromPool(GameObject prefab)
    {
        GameObject prefabInstance = null;
        List<GameObject> instancesList;
        if (pool.TryGetValue(GeneratePrefabInstancesName(prefab), out instancesList))
        {
            if (instancesList.Count != 0)
            {
                for (int i = 0; i < instancesList.Count; i++)
                {
                    if (instancesList[i].activeSelf != true)
                    {
                        prefabInstance = instancesList[i];
                        return prefabInstance;
                    }
                }
            }
        }

        return prefabInstance;
    }

    /// <summary>
    /// Returns an instance of the prefab.
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    private static GameObject Instantiate(GameObject prefab)
    {
        GameObject prefabInstance = Object.Instantiate(prefab);
        prefabInstance.name = GeneratePrefabInstancesName(prefab);
        return prefabInstance;
    }

    /// <summary>
    /// Returns a unique name to identify this prefab instances.
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    private static string GeneratePrefabInstancesName(GameObject prefab)
    {
        //Debug.Log("GeneratePrefabInstancesName " +prefab.name + prefab.GetInstanceID() + instancesOptionalNameEnding);
        return prefab.name + prefab.GetInstanceID() + instancesOptionalNameEnding;
    }
}