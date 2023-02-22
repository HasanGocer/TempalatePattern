using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitTouch : MonoBehaviour
{
    public bool isRival;

    private void OnTriggerEnter(Collider other)
    {
        ItemData.Field field = ItemData.Instance.field;
        PointText pointText = PointText.Instance;
        GhostManager ghostManager = GhostManager.Instance;

        if (other.CompareTag("Rival") && !isRival && ghostManager.mainHealth > 0)
        {
            GetComponent<CapsuleCollider>().enabled = false;
            RivalID rivalID = other.GetComponent<RivalID>();

            Vibration.Vibrate(30);
            StartCoroutine(HitShake(other.gameObject));
            pointText.CallPointText(other.gameObject, field.mainDamage, PointText.PointType.yellowHit);
            rivalID.characterBar.BarUpdate(field.rivalHealth, rivalID.rivalHealth, field.mainDamage);
            rivalID.rivalHealth -= field.mainDamage;
        }
        if (other.CompareTag("Main") && isRival)
        {
            GetComponent<CapsuleCollider>().enabled = false;

            StartCoroutine(HitShake(other.gameObject));
            pointText.CallPointText(other.gameObject, field.mainDamage, PointText.PointType.yellowHit);
            other.GetComponent<CharacterBar>().BarUpdate(field.mainHealth, ghostManager.mainHealth, field.rivalDamage);
            ghostManager.mainHealth -= field.rivalDamage;
        }
    }

    private IEnumerator HitShake(GameObject obj)
    {
        obj.transform.DOShakeScale(0.8f, 0.7f);
        yield return new WaitForSeconds(1f);
        obj.transform.localScale = new Vector3(3, 3, 3);
    }
}