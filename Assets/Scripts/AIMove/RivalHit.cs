using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalHit : MonoBehaviour
{
    [SerializeField] private RivalID rivalID;
    [SerializeField] private float gunReloadTime = 0.5f;
    [SerializeField] private Hit hit;
    bool lookMain;

    public IEnumerator SearchMain()
    {
        GhostManager ghostManager = GhostManager.Instance;

        while (rivalID.rivalAI.isLive)
        {
            yield return null;
            if (GameManager.Instance.gameStat == GameManager.GameStat.start)
                if (ItemData.Instance.field.rivalDistance > Vector3.Distance(gameObject.transform.position, ghostManager.mainPlayer.transform.position) && ghostManager.mainHealth > 0 && rivalID.rivalAI.isLive && !lookMain)
                {
                    lookMain = true;
                    rivalID.rivalAI.isSeeMain = true;
                    StartCoroutine(rivalID.rivalAI.Walk());
                    StartCoroutine(LookMain(ghostManager));
                    StartCoroutine(GunFire(ghostManager.mainPlayer));
                    yield return new WaitForSeconds(gunReloadTime);
                }
                else if (ItemData.Instance.field.rivalDistance < Vector3.Distance(gameObject.transform.position, ghostManager.mainPlayer.transform.position) && rivalID.rivalAI.isSeeMain)
                {
                    lookMain = false;
                    rivalID.rivalAI.isSeeMain = false;
                    rivalID.rivalAI.isFinish = false;
                }
        }
    }

    public IEnumerator NewStartRivalSearchMain()
    {
        GhostManager ghostManager = GhostManager.Instance;
        yield return null;

        while (rivalID.rivalAI.isLive && ghostManager.mainHealth > 0)
        {
            if (GameManager.Instance.gameStat == GameManager.GameStat.start)
                if (ItemData.Instance.field.rivalDistance > Vector3.Distance(gameObject.transform.position, ghostManager.mainPlayer.transform.position) && ghostManager.mainHealth > 0 && rivalID.rivalAI.isLive)
                {
                    print(31);
                    StartCoroutine(LookMain(ghostManager));
                    StartCoroutine(GunFire(ghostManager.mainPlayer));
                    yield return new WaitForSeconds(gunReloadTime);
                }
            yield return null;
        }
    }
    private IEnumerator LookMain(GhostManager ghostManager)
    {
        while (lookMain && rivalID.rivalAI.isLive)
        {
            transform.LookAt(ghostManager.mainPlayer.transform);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator GunFire(GameObject main)
    {
        while (lookMain && rivalID.rivalAI.isLive)
        {
            StartCoroutine(hit.HitPlayer(main.gameObject, 5));
            yield return new WaitForSeconds(gunReloadTime);
        }
    }
}
