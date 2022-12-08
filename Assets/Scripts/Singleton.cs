using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static Singleton instance = null;

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("Manager").AddComponent<Singleton>();
            }

            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
    }
}
