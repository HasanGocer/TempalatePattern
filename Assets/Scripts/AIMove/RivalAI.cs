using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RivalAI : MonoBehaviour
{
    public bool isLive = true, isSeeMain, isIssuse;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private List<GameObject> LookTargetObjects;
    [SerializeField] private RivalID rivalID;
    public bool isFinish = false;
    int forCount = 0;
    public void StartAI()
    {
        StartCoroutine(RunPath(objects, LookTargetObjects));
    }
    public IEnumerator Walk()
    {
        if (isLive && !isIssuse)
            rivalID.animController.CallWalkAnim();

        float lerpCount = 0;
        while (isLive)
        {
            StartCoroutine(ParticalSystem.Instance.CallWalkPartical(gameObject));
            lerpCount += Time.deltaTime * AIManager.Instance.walkFactor * 0.8f;
            transform.position = Vector3.Lerp(transform.position, GhostManager.Instance.mainPlayer.transform.position, lerpCount);
            yield return new WaitForSeconds(Time.deltaTime);
            if (10 > Vector3.Distance(transform.position, GhostManager.Instance.mainPlayer.transform.position))
            {
                if (isLive && !isIssuse)
                    rivalID.animController.CallIdleAnim();
                break;
            }
            if (!isSeeMain && isLive && !isIssuse)
            {
                rivalID.animController.CallIdleAnim();
                break;
            }
        }
    }
    public IEnumerator StartNewRivalWalk()
    {
        float lerpCount = 0;
        bool isOne = false;
        yield return null;

        while (isLive)
        {
            yield return null;
            if (10 > Vector3.Distance(transform.position, GhostManager.Instance.mainPlayer.transform.position))
            {
                if (isLive)
                {
                    rivalID.animController.CallIdleAnim();
                    isOne = false;
                }
            }
            else
            {
                if (!isOne)
                {
                    if (isLive)
                    {
                        rivalID.animController.CallWalkAnim();
                        StartCoroutine(ParticalSystem.Instance.CallWalkPartical(gameObject));
                    }
                    isOne = true;
                }
                lerpCount += Time.deltaTime * AIManager.Instance.walkFactor * 0.8f;
                transform.position = Vector3.Lerp(transform.position, GhostManager.Instance.mainPlayer.transform.position, lerpCount);
                transform.LookAt(GhostManager.Instance.mainPlayer.transform);
                yield return new WaitForSeconds(Time.deltaTime / 3);
            }
        }
    }

    private IEnumerator RunPath(List<GameObject> objects, List<GameObject> LookTargetObjects)
    {
        GameObject lastPos, lookPos;
        if (isLive && !isSeeMain && !isIssuse && GameManager.Instance.isStart)
            rivalID.animController.CallIdleAnim();

        if (objects.Count != 0)
            while (isLive)
            {
                yield return null;
                if (forCount == objects.Count)
                    forCount = 0;
                if (forCount >= objects.Count - 1)
                {
                    lastPos = objects[0];
                    lookPos = LookTargetObjects[0];
                }
                else
                {
                    if (forCount >= objects.Count - 1)
                    {
                        forCount = 0;
                        continue;
                    }
                    lastPos = objects[forCount + 1];
                    lookPos = LookTargetObjects[forCount + 1];
                }
                if (!isFinish && isLive && !isSeeMain && !isIssuse)
                {
                    if (forCount >= objects.Count - 1)
                    {
                        forCount = 0;
                        continue;
                    }
                    isFinish = true;
                    StartCoroutine(Walk(lastPos, lookPos, AIManager.Instance.walkFactor));
                }
            }
    }
    private IEnumerator Walk(GameObject Finish, GameObject lookpos, float factor)
    {
        float lerpCount = 0;
        if (isLive && !isSeeMain && !isIssuse)
            rivalID.animController.CallWalkAnim();
        StartCoroutine(LookWayPos(Finish, factor));
        while (isLive && !isSeeMain && !isIssuse)
        {
            StartCoroutine(ParticalSystem.Instance.CallWalkPartical(gameObject));
            lerpCount += Time.deltaTime * factor;
            this.transform.position = Vector3.Lerp(this.transform.position, Finish.transform.position, lerpCount);
            yield return new WaitForSeconds(Time.deltaTime / 2);
            if (3 > Vector3.Distance(transform.position, Finish.transform.position)) break;
        }
        StartCoroutine(LookPos(lookpos, factor));
    }
    private IEnumerator LookWayPos(GameObject Finish, float factor)
    {
        if (isLive && !isSeeMain && !isIssuse)
        {
            float lerpCount = 0;
            Quaternion first = this.transform.rotation;
            this.transform.LookAt(Finish.transform);
            Quaternion last = this.transform.rotation;
            this.transform.rotation = first;

            while (isLive && !isSeeMain && !isIssuse)
            {
                lerpCount += Time.deltaTime * factor * 100;
                this.transform.rotation = Quaternion.Lerp(first, last, lerpCount);
                yield return new WaitForSeconds(Time.deltaTime * factor * 100);
                if (5 > Quaternion.Angle(this.transform.rotation, last)) break;
            }
        }
    }
    private IEnumerator LookPos(GameObject lookPos, float factor)
    {
        if (isLive && !isSeeMain && !isIssuse)
        {
            float lerpCount = 0;
            Quaternion first = this.transform.rotation;
            this.transform.LookAt(lookPos.transform);
            Quaternion last = this.transform.rotation;
            this.transform.rotation = first;
            if (isLive && !isSeeMain && !isIssuse)
                rivalID.animController.CallIdleAnim();
            while (isLive && !isSeeMain && !isIssuse)
            {
                lerpCount += Time.deltaTime * factor * 100;
                this.transform.rotation = Quaternion.Lerp(first, last, lerpCount);
                yield return new WaitForSeconds(Time.deltaTime * factor * 100);
                if (5 > Quaternion.Angle(this.transform.rotation, last))
                {
                    yield return new WaitForSeconds(4);
                    forCount++;
                    isFinish = false;
                    break;
                }
            }
        }
    }
}
