using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTouch : MonoBehaviour
{
    [SerializeField] ItemSystem.ItemStat itemStat;
    [SerializeField] bool isPlus;
    [SerializeField] int itemCount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (itemStat == ItemSystem.ItemStat.item && isPlus)
            {
                PlayerManager.Instance.PlusPlayerItemCount(itemCount);
                gameObject.SetActive(false);
            }
            else if (itemStat == ItemSystem.ItemStat.money)
            {
                PlayerManager.Instance.PlusPlayerMoneyCount(itemCount);
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemStat == ItemSystem.ItemStat.item && !isPlus)
            {
                PlayerManager.Instance.PlusPlayerItemCount(-itemCount);
                other.enabled = false;
            }
        }
    }
}
