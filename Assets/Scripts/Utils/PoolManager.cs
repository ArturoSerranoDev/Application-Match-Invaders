﻿// ----------------------------------------------------------------------------
// PoolManager.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Retrieved instantiated objects from inactive pool. If needed, it creates more
// ----------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonPersistent<PoolManager>
{
    private static Dictionary<GameObject, Pool> pools = new Dictionary<GameObject, Pool>();
    
    void Init(GameObject prefab = null)
    {
        // if (pools == null)
        //     pools = new Dictionary<GameObject, Pool>();
            
        if (prefab != null && !pools.ContainsKey(prefab))
            pools[prefab] = new Pool(prefab, parent: this.transform);
    }
    
    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot, Transform parent = null)
    {
        Init(prefab);
        return pools[prefab].Spawn(pos, rot, parent);
    }
    
    public void Despawn(GameObject go)
    {
        PoolableObject poolableObject = go.GetComponent<PoolableObject>();
        if (poolableObject != null)
        {
            poolableObject.myPool.Despawn(go);
        }
    }

    public int GetMembersInPool(GameObject go)
    {
        if (!pools.ContainsKey(go))
            return 0;
        
        return pools[go].GetMembersInPool();
    }
    public int GetActiveMembersCount(GameObject go)
    {
        if (!pools.ContainsKey(go))
            return 0;
        
        return pools[go].GetActiveMembersCount();
    }
}

public class PoolableObject: MonoBehaviour
{
    public Pool myPool;
}


public class Pool
{
    List<GameObject> pooledObjects = new List<GameObject>();
    //Queue<GameObject> inactive = new Queue<GameObject>();

    GameObject prefab;
    GameObject poolParent;
    
    // Constructor
    public Pool(GameObject prefab, Transform parent)
    {
        this.prefab = prefab;
        this.poolParent = new GameObject();
        poolParent.transform.SetParent(parent, false);
        poolParent.name = prefab.name + " Container";
        pooledObjects = new List<GameObject>();
    }
    
    public GameObject Spawn(Vector3 pos, Quaternion rot, Transform parent = null)
    {
        GameObject go = null;
        
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                go = pooledObjects[i];
            }
        }

        if (go == null)
        {
            go = MonoBehaviour.Instantiate(prefab);
            pooledObjects.Add(go);
            go.AddComponent<PoolableObject>().myPool = this;

        }
        
        go.transform.position = pos;
        go.transform.rotation = rot;
        go.transform.SetParent(poolParent.transform, false);
        go.gameObject.SetActive(true);

        return go;
    }
    
    public void Despawn(GameObject despawnGO)
    {
        despawnGO.SetActive(false);
    }

    public int GetMembersInPool()
    {
        return pooledObjects.Count;
    }

    public int GetActiveMembersCount()
    {
        int count = 0;
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                count++;
            }
        }

        return count;
    }

}