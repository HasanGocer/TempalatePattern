using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHit : MonoBehaviour
{
    [SerializeField] private float gunReloadTime = 0.3f;
    public Hit hit;
    public bool isMainLive = true;
    public PlayerMovement playerMovment;
    public bool isRivalDead = false;
    int focusRivalCount = 0;
    bool isLook;
    List<RivalID> rivalIDs;

    public IEnumerator SearchMain()
    {
        FinishSystem finishSystem = FinishSystem.Instance;
        ItemData itemData = ItemData.Instance;

        while (GhostManager.Instance.mainHealth > 0 && isMainLive)
        {
            yield return null;
            if (GameManager.Instance.gameStat == GameManager.GameStat.start)
                for (int i = 0; i < rivalIDs.Count; i++)
                {
                    yield return null;
                    RivalID rivalID = rivalIDs[i];
                    if (Vector3.Distance(rivalIDs[focusRivalCount].gameObject.transform.position, transform.position) > Vector3.Distance(rivalID.gameObject.transform.position, transform.position) && Vector3.Distance(rivalID.gameObject.transform.position, transform.position) < itemData.field.mainDistance && rivalID.rivalAI.isLive)
                        focusRivalCount = i;
                    if (itemData.field.mainDistance > Vector3.Distance(transform.position, rivalID.gameObject.transform.position))
                    {
                        if (rivalID.rivalAI.isLive && (!isRivalDead || focusRivalCount == i))
                        {
                            isRivalDead = true;
                            focusRivalCount = i;
                            //StartCoroutine(LookMain(rivalID.gameObject));
                            StartCoroutine(GunFire(rivalID.gameObject, itemData));
                            yield return new WaitForSeconds(gunReloadTime);
                        }
                        else if (!rivalID.rivalAI.isLive && (!isRivalDead || focusRivalCount == i))
                        {
                            isRivalDead = false;
                        }
                        else if (Vector3.Distance(rivalIDs[focusRivalCount].gameObject.transform.position, transform.position) > itemData.field.mainDistance)
                        {
                            isRivalDead = false;
                        }
                    }
                }
        }
    }
    public IEnumerator GunFire(GameObject main, ItemData itemData)
    {
        StartCoroutine(hit.HitPlayer(main, itemData.field.mainDamageSpeed));
        yield return new WaitForSeconds(gunReloadTime);
    }

    private IEnumerator LookMain(GameObject obj)
    {
        if (!isLook)
        {
            print(obj.transform.position);
            isLook = true;
            //rivali gördüğü sürece diye düzelt

            while (true)
            {
                transform.LookAt(obj.transform);
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
                yield return new WaitForEndOfFrame();
            }
            isLook = false;
        }

    }
}