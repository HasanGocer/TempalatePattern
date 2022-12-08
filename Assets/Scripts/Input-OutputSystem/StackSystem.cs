using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackSystem : MonoBehaviour
{
    [SerializeField] private float stackTime, DropTime;
    [SerializeField] private float stackDistance, dropDistance;
    [SerializeField] private float stackMoveTime, dropMoveTime;
    [SerializeField] private int stackMaximumCount;
    [SerializeField] private bool stackTransfer, dropTransfer;
    [SerializeField] private GameObject stackParent, stackPos;

    public List<GameObject> Objects = new List<GameObject>();

    private void Start()
    {
        stackTransfer = false;
        dropTransfer = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Input"))
        {
            if (Objects.Count < stackMaximumCount && stackTransfer == false)
            {
                stackTransfer = true;
                StartCoroutine(StackAdd(other));
            }
        }
        else if (other.CompareTag("Output"))
        {
            if (dropTransfer == false)
            {
                dropTransfer = true;
                StartCoroutine(StackDrop(other));
            }
        }
    }

    IEnumerator StackAdd(Collider other)
    {
        FirstSpawn FS = other.GetComponent<FirstSpawn>();
        if (FS.Objects.Count - 1 >= 0)
        {
            GameObject obj = FS.Objects[FS.Objects.Count - 1];
            FS.Objects.RemoveAt(FS.Objects.Count - 1);
            obj.transform.SetParent(stackParent.transform);
            Vector3 pos = new Vector3(stackPos.transform.position.x, stackPos.transform.position.y + stackDistance * Objects.Count, stackPos.transform.position.z);
            Jump2(obj, pos);
            //obj.transform.DOMove(pos, stackMoveTime);//tam yere götürmesi için bir þey lazým
            Objects.Add(obj);
            yield return new WaitForSeconds(stackTime);
        }
        stackTransfer = false;
        yield return null;
    }

    IEnumerator StackDrop(Collider other)
    {
        FinishStack FS = other.GetComponent<FinishStack>();
        if (Objects.Count - 1 >= 0)
        {
            GameObject obj = Objects[Objects.Count - 1];
            Objects.RemoveAt(Objects.Count - 1);
            obj.transform.SetParent(FS.dropParent.transform);
            Vector3 pos = new Vector3(FS.dropPos.transform.position.x, FS.dropPos.transform.position.y + dropDistance * FS.Objects.Count, FS.dropPos.transform.position.z);
            obj.transform.DOMove(pos, dropMoveTime);//tam yere götürmesi için bir þey lazým
            FS.Objects.Add(obj);
            yield return new WaitForSeconds(DropTime);
        }
        dropTransfer = false;
        yield return null;
    }

    private void Jump2(GameObject obj, Vector3 pos)
    {
        Vector3 tempPos = obj.transform.localPosition;

        obj.transform.localPosition = tempPos;
        obj.transform.DOMove(pos, stackTime);
        tempPos = obj.transform.localPosition;
    }
}
