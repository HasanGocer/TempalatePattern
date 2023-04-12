using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTouch : MonoBehaviour
{
    [SerializeField] ItemSystem.PlaneStat planeStat;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (planeStat == ItemSystem.PlaneStat.win)
                PlayerController.Instance.isFree = false;
            else
            {
                GameManager.Instance.gameStat = GameManager.GameStat.finish;
                Buttons.Instance.failPanel.SetActive(true);
            }
        }
    }
}
