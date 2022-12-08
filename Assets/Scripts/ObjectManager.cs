using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoSingleton<ObjectManager>
{
    //managerde bulunacak

    [System.Serializable]
    public class Object›nGame
    {
        GameObject[] gameObject›nGame;
    }
    public Object›nGame object›nGame;

    public int GameObjectCount;
}
