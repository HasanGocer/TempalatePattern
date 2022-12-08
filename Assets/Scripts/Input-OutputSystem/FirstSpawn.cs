using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstSpawn : MonoBehaviour
{
    [SerializeField] private float spawnTime, spawnDistance;
    [SerializeField] private int objectCount, TimerSpeed, itemCount;

    [SerializeField] private GameObject spawnPosition, pos2;

    public List<GameObject> Objects = new List<GameObject>();

    private void Start()
    {
        GamesManager.Instance.inGame = true;
        StartCoroutine(ItemSpawn());
    }

    IEnumerator ItemSpawn()
    {
        if (GamesManager.Instance.inGame)
        {
            while (true)
            {
                if (objectCount > Objects.Count && 0 < ObjectPool.Instance.IDCount(itemCount))
                {
                    GameObject obj = ObjectPool.Instance.GetPooledObject(itemCount);
                    Vector3 pos = new Vector3(spawnPosition.transform.position.x,
                        spawnPosition.transform.position.y + (Objects.Count * spawnDistance),
                        spawnPosition.transform.position.z);
                    obj.transform.position = pos;
                    //Jump1(obj, pos2.transform.position);
                    //Jump2(obj, pos2.transform.position);
                    //obj.transform.DOMove(pos, TimerSpeed);
                    Objects.Add(obj);
                    yield return new WaitForSeconds(spawnTime);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }

    private void Jump1(GameObject obj, Vector3 pos)
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime / TimerSpeed;
            obj.transform.position = Vector3.Lerp(obj.transform.localPosition, pos, Time.deltaTime * TimerSpeed);
            if (obj.transform.position == pos)
            {
                break;
            }
        }
    }

    private void Jump2(GameObject obj, Vector3 pos)
    {
        Vector3 tempPos = obj.transform.localPosition;

        while (true)
        {
            obj.transform.localPosition = tempPos;
            obj.transform.DOMove(pos, TimerSpeed);
            tempPos = obj.transform.localPosition;
            if (obj.transform.localPosition == pos)
            {
                break;
            }
        }
    }
}
