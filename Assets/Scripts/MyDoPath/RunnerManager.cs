using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RunnerManager : MonoSingleton<RunnerManager>
{
    public int _OPRunnerCount;
    public GameObject _runnerPos;

    public List<GameObject> Runner = new List<GameObject>();

    public IEnumerator StartRunner()
    {
        for (int i = 0; i < ItemData.Instance.field.runnerCount; i++)
        {
            GameObject obj = ObjectPool.Instance.GetPooledObject(_OPRunnerCount);
            obj.transform.position = _runnerPos.transform.position;
            obj.GetComponent<PathSelection>().pathSelection = 0;
            Runner.Add(obj);
            MyDoPath.Instance.StartNewRunner(obj);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void NewStartRunner()
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPRunnerCount);
        obj.transform.position = _runnerPos.transform.position;
        obj.GetComponent<PathSelection>().pathSelection = 0;
        Runner.Add(obj);
        MyDoPath.Instance.StartNewRunner(obj);
    }

    public IEnumerator SpeedUp()
    {
        DOTween.PauseAll();
        for (int i = 0; i < ItemData.Instance.field.runnerCount; i++)
        {
            Runner[i].transform.DOTogglePause();
            ObjectPool.Instance.AddObject(_OPRunnerCount, Runner[i]);
            GameObject obj = ObjectPool.Instance.GetPooledObject(_OPRunnerCount);
            obj.transform.position = _runnerPos.transform.position;
            obj.GetComponent<PathSelection>().pathSelection = 0;
            Runner[i] = obj;
            MyDoPath.Instance.StartNewRunner(obj);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
