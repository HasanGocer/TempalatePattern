using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoadSingleton<T> : MonoBehaviour where T : LoadSingleton<T>
{
    private static T instance;
    private static object lockObject = new object(); // Thread-safe bir kod yazmak için kilit nesnesi

    public static T Instance
    {
        get
        {
            // Ýlk kontrol, instance'ýn null olup olmadýðýný kontrol eder. Eðer null ise
            // FindObjectOfType ile instance'ý bulmaya çalýþýr. Bulamazsa, CreateInstance metodu ile instance
            // oluþturur.
            if (instance == null)
            {
                lock (lockObject) // Thread-safe bir kod yazmak için kilit nesnesi
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();

                        if (instance == null)
                        {
                            instance = CreateInstance();
                        }
                    }
                }
            }

            return instance;
        }
    }

    private static T CreateInstance()
    {
        GameObject singletonObject = new GameObject();
        T singleton = singletonObject.AddComponent<T>();
        singletonObject.name = typeof(T).ToString();

        return singleton;
    }

    protected virtual void Awake()
    {
        // instance henüz atanmadýysa, instance'ý bu MonoBehavior nesnesi olarak atar ve sahne deðiþikliklerinde yok
        // olmamasý için DontDestroyOnLoad'u kullanýr.
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        // Eðer instance bu MonoBehavior nesnesi deðilse, bu nesne yok edilir.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        // Bu MonoBehavior nesnesi instance olarak atanmýþ ise, instance'ý null olarak ayarlar.
        if (instance == this)
        {
            instance = null;
        }
    }
}