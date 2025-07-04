﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPoolManager : UnitySingleton<ObjectPoolManager>
{
    static Dictionary<int, nObjectPool> PoolDict = new Dictionary<int, nObjectPool>();

    public static nObjectPool GetObjectPool(PoolableObject poolableObject, int defaultSize = DEFAULT_POOL_SIZE)
    {
        int poolKey = poolableObject.poolKey;
        if (poolableObject.gameObject.scene.name == null)
        {
            poolKey = poolableObject.GetInstanceID();
        }

        nObjectPool poolDictCache;
        bool isHasPool = PoolDict.TryGetValue(poolKey, out poolDictCache);

        if (!isHasPool)
        {
            poolDictCache = CreateNewObjectPool(poolableObject, defaultSize);
        }

        return poolDictCache;
    }

    public static PoolableObject GetObject(PoolableObject poolableObject, Vector3 pos, Quaternion rot, Transform parent = null)
    {
        return GetObjectPool(poolableObject).ReUse(pos, rot, parent);
    }
    public static T GetObject<T>(PoolableObject poolableObject, Vector3 pos, Quaternion rot, Transform parent = null) where T : Component
    {
        return GetObjectPool(poolableObject).ReUse<T>(pos, rot, parent);
    }

    public static T GetObject<T>(PoolableObject poolableObject, Transform parent = null) where T : Component
    {
        var transformCache = poolableObject.transformCache;

        if (!transformCache)
        {
            Debug.Log("This transformCache is null");
        }
        return GetObjectPool(poolableObject).ReUse<T>(transformCache.position, transformCache.rotation, parent);
    }

    const int DEFAULT_POOL_SIZE = 3;
    static nObjectPool CreateNewObjectPool(PoolableObject refIPoolable, int size = DEFAULT_POOL_SIZE)
    {
        nObjectPool pool = Instance.gameObject.AddComponent<nObjectPool>();
        pool.Init(refIPoolable, size);
        Debug.LogFormat("[PoolManager][CreateNewPool] 創建新池: {0}", pool.poolKey);
        PoolDict.Add(pool.poolKey, pool);
        return pool;
    }

    public static void ClearAll()
    {
        foreach (var item in PoolDict)
        {
            item.Value.RecoveryAll();
        }
    }
}