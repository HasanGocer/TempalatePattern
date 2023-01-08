using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class AnimControl : MonoSingleton<AnimControl>
{
    [SerializeField] private AnimancerComponent mainAnim, enemyAnim;
    [SerializeField] private AnimationClip waiting, boxing1, boxing2, beating1, beating2, kick1, kick2, death, win, ýdle;
    [SerializeField] private Material _deadPlayerMaterial;
    public GameObject finishHim;

    public void StartAnimencer()
    {
        mainAnim.Play(waiting);
        enemyAnim.Play(waiting);
    }

    public IEnumerator CallHitRival(int count)
    {
        enemyAnim.Play(boxing1, 0.3f);
        yield return new WaitForSeconds(0.55f);
        StartCoroutine(ParticalSystem.Instance.CallHitCharacter(false, count));
        if (GameManager.Instance.isStart)
            mainAnim.Play(beating1, 0.1f);
        yield return new WaitForSeconds(1f);
        if (GameManager.Instance.isStart)
        {
            mainAnim.Play(waiting, 0.3f);
            enemyAnim.Play(waiting, 0.3f);
        }
    }

    public IEnumerator CallHitPlayer(int ID, int count)
    {
        if (ID == 0)
            mainAnim.Play(boxing1, 0.3f);
        else if (ID == 1)
            mainAnim.Play(boxing1, 0.3f);
        else
            mainAnim.Play(kick2, 0.3f);

        yield return new WaitForSeconds(0.55f);
        StartCoroutine(ParticalSystem.Instance.CallHitCharacter(true, count));
        if (GameManager.Instance.isStart)
            enemyAnim.Play(beating2, 0.1f);
        yield return new WaitForSeconds(1f);
        if (GameManager.Instance.isStart)
        {
            enemyAnim.Play(waiting, 0.3f);
            mainAnim.Play(waiting, 0.3f);
        }
    }

    public void CallRivalWin()
    {
        enemyAnim.Play(win, 0.3f);
        mainAnim.Play(death, 0.1f);
        mainAnim.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = _deadPlayerMaterial;
    }

    public void CallPlayerWin()
    {
        mainAnim.Play(win, 0.3f);
        enemyAnim.Play(death, 0.1f);
        enemyAnim.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = _deadPlayerMaterial;
    }

    public IEnumerator FinishHim()
    {
        finishHim.SetActive(true);
        yield return new WaitForSeconds(1);
        finishHim.SetActive(false);
    }

    public void CallMarketAnim()
    {
        MarketSystem.Instance.marketMainPlayer.GetComponent<AnimancerComponent>().Play(ýdle);
    }
}
