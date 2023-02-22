using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RunnerManager : MonoSingleton<RunnerManager>
{
    [Header("Walker_Field")]
    [Space(10)]

    [SerializeField] int _OPRunnerCount;
    [SerializeField] int levelModRunnerPlusCount;
    [SerializeField] float _spawnCoundownTime;

    public List<GameObject> Walker = new List<GameObject>();

    public IEnumerator StartWalkerWalk(int walkerCount, int PathCount, ItemData itemData)
    {
        for (int i1 = itemData.field.walkerTypeCount; i1 >= 0; i1--)
        {
            MyDoPath.Instance.walkerCount += walkerCount + (levelModRunnerPlusCount * i1);
            for (int i2 = 0; i2 < walkerCount + (levelModRunnerPlusCount * i1); i2++)
            {
                GameObject obj = GetObject(i1);
                WalkerID walkerID = obj.GetComponent<WalkerID>();

                WalkerStatPlacement(obj, walkerID, PathCount, i1, itemData.field.walkerHealth - (i1 * itemData.constant.walkerHealth));
                yield return new WaitForSeconds(_spawnCoundownTime);
            }
        }
    }

    public void RemoveWalker(GameObject walker)
    {
        Walker.Remove(walker);
        ObjectPool.Instance.AddObject(_OPRunnerCount + walker.GetComponent<WalkerID>().ID, walker);
    }

    private GameObject GetObject(int ID)
    {
        return ObjectPool.Instance.GetPooledObject(_OPRunnerCount + ID);
    }
    private void WalkerStatPlacement(GameObject obj, WalkerID walkerID, int PathCount, int ID, int health)
    {
        Walker.Add(obj);
        walkerID.pathSelection = PathCount;
        walkerID.StartWalkerID(ID, health);
        MyDoPath.Instance.StartNewRunner(obj, walkerID, walkerID.pathSelection);
    }
}
