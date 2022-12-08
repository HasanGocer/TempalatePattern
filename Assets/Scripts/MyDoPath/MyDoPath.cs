using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyDoPath : MonoSingleton<MyDoPath>
{
    [System.Serializable]
    public struct Balls
    {
        public float length;
        public List<GameObject> BallsGO;
        public Vector3[] BallsV3;
    }
    public Balls[] Ball;

    private int Ways;
    public float runnerTime;

    private void Awake()
    {
        Ways = Ball.Length;
        for (int i1 = 0; i1 < Ways; i1++)
        {
            Ball[i1].length = 0;
            Ball[i1].BallsV3 = new Vector3[Ball[i1].BallsGO.Count + 1];
            for (int i2 = 0; i2 < Ball[i1].BallsGO.Count - 1; i2++)
            {
                Ball[i1].length += Vector3.Distance(Ball[i1].BallsGO[i2].transform.position, Ball[i1].BallsGO[i2 + 1].transform.position);
                Ball[i1].BallsV3[i2] = Ball[i1].BallsGO[i2].transform.position;
                if (i2 == Ball[i1].BallsGO.Count - 2)
                    Ball[i1].BallsV3[i2 + 1] = Ball[i1].BallsGO[i2 + 1].transform.position;
            }
            Ball[i1].length += Vector3.Distance(Ball[i1].BallsGO[Ball[i1].BallsV3.Length - 2].transform.position, RunnerManager.Instance._runnerPos.transform.position);
            Ball[i1].BallsV3[Ball[i1].BallsV3.Length - 1] = RunnerManager.Instance._runnerPos.transform.position;

        }
    }

    //Kullanýlmýyor
    /*
    public void StartRun()
    {
        for (int i = 0; i < ItemData.Instance.field.runnerCount; i++)
        {
            GameObject obj = ObjectPool.Instance.GetPooledObject(RunnerManager.Instance._OPrunnerCount);
            RunnerManager.Instance.Runner.Add(obj);
            runnerTime = length[obj.GetComponent<PathSelection>().pathSelection] * ItemData.Instance.field.runnerSpeed;

            Vector3[] pos = new Vector3[Balls[obj.GetComponent<PathSelection>().pathSelection].Count];
            for (int i1 = 0; i1 < pos.Length; i1++)
            {
                pos[i1] = BallsWP[obj.GetComponent<PathSelection>().pathSelection, i1];
            }
            obj.transform.DOPath(pos, runnerTime, PathType.CatmullRom).SetEase(Ease.InOutSine).Loops();
        }
    }*/

    public void StartNewRunner(GameObject obj)
    {
        runnerTime = Ball[obj.GetComponent<PathSelection>().pathSelection].length * ItemData.Instance.field.runnerSpeed;

        obj.transform.DOPath(Ball[obj.GetComponent<PathSelection>().pathSelection].BallsV3, runnerTime, PathType.CatmullRom, PathMode.Full3D, 20, Color.black).SetEase(Ease.Linear).SetLoops(1000);
    }
}
