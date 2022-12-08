using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackSystem : MonoSingleton<StackSystem>
{
    //Playerde bulunacak

    [SerializeField] private float _stackDistance;
    [SerializeField] private float _stackMoveTime, _dropMoveTime;
    [SerializeField] private int _stackMaximumCount;
    [SerializeField] private GameObject _stackParent, _dropParent;
    [SerializeField] private GameObject _stackPos, _dropPos;
    [SerializeField] private int _OPTrashCount;

    public List<GameObject> Objects = new List<GameObject>();

    private void Start()
    {
        GameManager.Instance.dropTransfer = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Input"))
        {
            if (Objects.Count < _stackMaximumCount)
            {
                StartCoroutine(StackAdd(other));
            }
        }
        else if (other.CompareTag("Output"))
        {
            if (!GameManager.Instance.dropTransfer == false)
            {
                StartCoroutine(other.GetComponent<WaitSystem>().bar(this.gameObject));
            }
        }
    }

    IEnumerator StackAdd(Collider other)
    {
        //türlere göre ayýrým yap
        other.transform.SetParent(_stackParent.transform);
        Vector3 pos = new Vector3(_stackPos.transform.position.x, _stackPos.transform.position.y + _stackDistance * Objects.Count, _stackPos.transform.position.z);
        other.transform.transform.DOLocalMove(pos, _stackMoveTime);
        other.GetComponent<ObjectTouchPlane>().StackÝnPlayer();
        Objects.Add(other.gameObject);
        yield return new WaitForSeconds(_stackMoveTime);
    }

    public IEnumerator StackDrop(GameObject place)
    {
        WaitSystem waitSystem = place.GetComponent<WaitSystem>();
        int placeCount = 0;
        while (waitSystem.inPlace && placeCount < Objects.Count && !GameManager.Instance.dropTransfer)
        {
            GameManager.Instance.dropTransfer = true;
            if (waitSystem.placeCount == Objects[placeCount].GetComponent<ObjectTouchPlane>().objectCount)
            {
                GameObject obj = Objects[placeCount];
                Objects.RemoveAt(placeCount);
                obj.transform.SetParent(_dropParent.transform);
                Vector3 pos = new Vector3(_dropPos.transform.position.x, _dropPos.transform.position.y, _dropPos.transform.position.z);
                obj.transform.DOLocalMove(pos, _dropMoveTime);
                yield return new WaitForSeconds(_dropMoveTime);
                obj.transform.GetChild(waitSystem.placeCount).gameObject.SetActive(false);
                ObjectPool.Instance.AddObject(_OPTrashCount, obj);
            }
            yield return null;

            placeCount++;
            GameManager.Instance.dropTransfer = false;
        }

        for (placeCount = 0; placeCount < Objects.Count - 1; placeCount++)
        {
            int distanceCount = 0;
            if (Objects[placeCount] == null)
            {
                distanceCount++;
                Objects[placeCount].transform.DOLocalMove(Objects[placeCount + distanceCount].transform.position, 0.1f);
                Objects[placeCount] = Objects[placeCount + distanceCount];
            }
        }
    }
}
