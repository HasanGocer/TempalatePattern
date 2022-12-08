using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoSingleton<AIManager>
{
    [System.Serializable]
    public class Stacker›nGame
    {
        public List<GameObject> gameObjectStacker = new List<GameObject>();
        public List<bool> boolStacker = new List<bool>();
        public GameObject stackOutPlace;
    }
    public Stacker›nGame[] stackerInGame;

    public float AIDistanceConstant;
    public int stackDistance;
    public int maxStackerCount;
    public int maxStackerTypeCount;

    public void StartPlace(int AITypeCount, int[] AICount)
    {
        for (int i1 = 0; i1 < AITypeCount; i1++)
        {
            for (int i2 = 0; i2 < AICount[i1]; i2++)
            {
                stackerInGame[i1].boolStacker.Add(true);
                stackerInGame[i1].gameObjectStacker[i2].SetActive(true);
                //AI Áal˝˛t˝rma kodu
            }
        }
    }
}
