using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyDoPath : MonoSingleton<MyDoPath>
{
    [System.Serializable]
    public class Ways
    {
        public List<GameObject> WaysGO = new List<GameObject>();
    }


    [Header("Path_Way")]
    [Space(10)]

    [SerializeField] List<Ways> _ways;
    [SerializeField] float _walkShakeScalePower;
    [SerializeField] float _maxWalkerDisance, _maxWalkerQuaternionDistance;
    [SerializeField] float _speedFactor, _quaternionFactor;

    [Header("Path_Way_Field")]
    [Space(10)]

    public int walkerCount = 0;

    public void FirstSpawn()
    {
        ItemData itemData = ItemData.Instance;

        if (GameManager.Instance.level % 10 != 0)
        {
            for (int i = 0; i < _ways.Count; i++)
                StartCoroutine(RunnerManager.Instance.StartWalkerWalk(itemData.field.walkerCount, i, itemData));
        }
        else
        {
            RunnerManager.Instance.StartBossWalk();
        }
    }

    public void StartNewBoss(GameObject boss, WalkerID walkerID)
    {
        WalkerPlacement(ref boss, _ways[0].WaysGO[0]);
        StartCoroutine(WalkPart(boss, _ways[0].WaysGO, _speedFactor, walkerID, _maxWalkerDisance, true));
    }

    public void StartNewRunner(GameObject walker, WalkerID walkerID, int wayCount)
    {
        WalkerPlacement(ref walker, _ways[wayCount].WaysGO[0]);
        StartCoroutine(WalkPart(walker, _ways[wayCount].WaysGO, _speedFactor, walkerID, _maxWalkerDisance, false));
    }

    public void WalkerPlacement(ref GameObject walker, GameObject pos)
    {
        walker.transform.position = pos.transform.position;
    }

    private IEnumerator WalkPart(GameObject walker, List<GameObject> pos, float factor, WalkerID walkerID, float maxWalkerDisance, bool isBoss, int wayCount = 0)
    {

        StartCoroutine(LookNewWaypoint(walker, pos[wayCount + 1], walkerID));

        while (walkerID.isLive && GameManager.Instance.gameStat == GameManager.GameStat.start)
        {
            Vector3 direction = (pos[wayCount + 1].transform.position - walker.transform.position).normalized;
            if (!isBoss)
                walker.transform.position += direction * factor * Time.deltaTime;
            else
                walker.transform.position += direction * factor * Time.deltaTime / 3;
            walker.transform.DOShakeScale(Time.deltaTime / 2, _walkShakeScalePower);
            yield return new WaitForSeconds(Time.deltaTime);
            if (maxWalkerDisance > Vector3.Distance(walker.transform.position, pos[wayCount + 1].transform.position)) break;
        }
        wayCount++;
        if (wayCount <= pos.Count - 2 && walkerID.isLive && GameManager.Instance.gameStat == GameManager.GameStat.start) StartCoroutine(WalkPart(walker, pos, factor, walkerID, maxWalkerDisance, isBoss, wayCount));
    }
    private IEnumerator LookNewWaypoint(GameObject walker, GameObject newWayPoint, WalkerID walkerID)
    {
        float lerpCount = 0;
        Quaternion finishRotation;
        SetQuaternion(walker, newWayPoint, out finishRotation);

        while (walkerID.isLive && GameManager.Instance.gameStat == GameManager.GameStat.start)
        {
            lerpCount += Time.deltaTime * _quaternionFactor;
            walker.transform.rotation = Quaternion.Lerp(walker.transform.rotation, finishRotation, lerpCount);
            yield return new WaitForSeconds(Time.deltaTime);
            if (_maxWalkerQuaternionDistance > Quaternion.Angle(walker.transform.rotation, finishRotation)) break;
        }
    }
    private void SetQuaternion(GameObject walker, GameObject newWayPoint, out Quaternion finishRotation)
    {
        Quaternion nowRotation = walker.transform.rotation;
        walker.transform.LookAt(newWayPoint.transform);
        finishRotation = walker.transform.rotation;
        walker.transform.rotation = nowRotation;
    }
}
