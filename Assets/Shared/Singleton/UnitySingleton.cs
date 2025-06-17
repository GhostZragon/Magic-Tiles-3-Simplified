﻿using UnityEngine;

//namespace CloudMacaca
//{
public class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private bool DontDestroyOnLoadWhenInit = false;
    static T instance;

    static object lockObj = new object();

    protected virtual void Awake()
    {
        if (DontDestroyOnLoadWhenInit)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public static T Instance
    {
        get
        {
            // if (applicationIsQuitting)
            // {
            //     Debug.LogWarningFormat("[Singleton] Instance {0} have destroyed (Maybe application quit)",
            //         typeof(T));

            //     return null;
            // }

            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = (T)FindFirstObjectByType(typeof(T));

                        int howManyObjOfType = FindObjectsByType(typeof(T), FindObjectsSortMode.None).Length;

                        if (howManyObjOfType == 1)
                        {
                            Debug.LogFormat("[Singleton] {0} was created", instance);
                        }
                        else if (howManyObjOfType > 1)
                        {
                            Debug.LogErrorFormat("[Singleton] {0} already has {1} in the scene", typeof(T),
                                howManyObjOfType);
                        }
                        else
                        {
                            GameObject singleton = new GameObject();
                            instance = singleton.AddComponent<T>();
                            singleton.name = string.Format("Singleton_{0}", typeof(T));
                            
                            DontDestroyOnLoad(singleton);

                            Debug.LogWarningFormat(
                                "[Singleton] Use Lazy Initialization\n{1} was created with DontDestroyOnLoad",
                                typeof(T),
                                instance);
                        }
                    }
                }
            }

            return instance;
        }
    }

    static bool applicationIsQuitting = false;

    protected virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
//}