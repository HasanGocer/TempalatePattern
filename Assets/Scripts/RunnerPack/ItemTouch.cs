using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTouch : MonoBehaviour
{
    [SerializeField] ItemSystem.ItemStat itemStat;
    [SerializeField] int itemCount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (itemStat == ItemSystem.ItemStat.item)
            {
                PlayerManager.Instance.PlusPlayerItemCount(itemCount);
                gameObject.SetActive(false);
            }
            else
            {
                PlayerManager.Instance.PlusPlayerMoneyCount(itemCount);
                gameObject.SetActive(false);
            }
        }
    }
}
