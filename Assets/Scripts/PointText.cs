using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PointText : MonoSingleton<PointText>
{
    [SerializeField] private int moneyint, researchPointint;
    [SerializeField] private float textMoveTime;
    [SerializeField] private float moneyJumpDistance, reserarchJumpDistance;
    [SerializeField] Ease moveEaseType;

    public IEnumerator CallPointMoneyText(GameObject Pos)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(moneyint);
        obj.GetComponent<TMP_Text>().text = ItemData.Instance.field.addedMoney.ToString();
        obj.transform.position = Pos.transform.position;
        obj.transform.DOMove(new Vector3(Pos.transform.position.x, Pos.transform.position.y + moneyJumpDistance, Pos.transform.position.z), textMoveTime).SetEase(moveEaseType);
        yield return new WaitForSeconds(textMoveTime);
        ObjectPool.Instance.AddObject(moneyint, obj);
    }

    public IEnumerator CallPointResearchText(GameObject Pos)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(researchPointint);
        obj.GetComponent<TMP_Text>().text = ItemData.Instance.field.addedResearchPoint.ToString();
        obj.transform.position = Pos.transform.position;
        obj.transform.DOMove(new Vector3(Pos.transform.position.x, Pos.transform.position.y + reserarchJumpDistance, Pos.transform.position.z), textMoveTime).SetEase(moveEaseType);
        yield return new WaitForSeconds(textMoveTime);
        ObjectPool.Instance.AddObject(researchPointint, obj);
    }
}
