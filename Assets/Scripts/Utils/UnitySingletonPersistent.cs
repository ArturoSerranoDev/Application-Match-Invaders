// ----------------------------------------------------------------------------
// SingletonPersistent.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Allows child classes to be used as a single instance between scenes
// ----------------------------------------------------------------------------
using UnityEngine;

public abstract class UnitySingletonPersistent<T> : MonoBehaviour where T : Component
{
    static bool isApplicationQuitting;
    static T instance;
    
    public static T Instance
    {
        get
        {
            if (isApplicationQuitting)
                return null;
            
            if (instance == null)
            {
                // Check if other GameObjects in the Scene already have this Component
                instance = FindObjectOfType<T>();
                
                if (instance == null)
                {
                    GameObject obj = new GameObject { name = typeof(T).Name };
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }
    
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("UnitySingleton Destroy: " + instance.GetType().Name);
            Destroy(gameObject);
        }
    }
        
    void OnApplicationQuit()
    {
        isApplicationQuitting = true;
    }
}

public abstract class UnitySingleton<T> : MonoBehaviour where T : Component
{
    static bool isApplicationQuitting;
    static T instance;
    
    public static T Instance
    {
        get
        {
            if (isApplicationQuitting)
                return null;

            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject { name = typeof(T).Name };
                    instance = obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnApplicationQuit()
    {
        isApplicationQuitting = true;
    }
}
