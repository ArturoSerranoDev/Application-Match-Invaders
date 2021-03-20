// ----------------------------------------------------------------------------
// SingletonPersistent.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Allows child classes to be used as a single instance between scenes
// ----------------------------------------------------------------------------
using UnityEngine;

public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    static T instance;
    
    public static T Instance
    {
        get
        {
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
}
