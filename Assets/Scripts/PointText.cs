using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PointText : MonoSingleton<PointText>
{
    [SerializeField] private int _OPMoneyInt;
    [SerializeField] private float _textMoveTime;
    [SerializeField] private float _moneyJumpDistance;
    [SerializeField] Ease _moveEaseType = Ease.InOutBounce;

    public IEnumerator CallPointMoneyText(GameObject Pos, int count, bool isCorrect = true)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPMoneyInt);
        if (isCorrect) obj.GetComponent<TMP_Text>().color = Color.green;
        else obj.GetComponent<TMP_Text>().color = Color.red;
        obj.GetComponent<TMP_Text>().text = count.ToString();
        obj.transform.position = Pos.transform.position;
        obj.transform.DOMove(new Vector3(Pos.transform.position.x, Pos.transform.position.y + _moneyJumpDistance, Pos.transform.position.z), _textMoveTime).SetEase(_moveEaseType);
        yield return new WaitForSeconds(_textMoveTime);
        ObjectPool.Instance.AddObject(_OPMoneyInt, obj);
    }
}
