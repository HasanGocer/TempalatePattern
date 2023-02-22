using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalID : MonoBehaviour
{
    public int rivalHealth;
    public CapsuleCollider capsuleCollider;
    public CharacterBar characterBar;
    public RivalAI rivalAI;
    public Hit hit;
    public AnimController animController;
    public RivalHit rivalHit;
    public GameObject brokenBox;
    public GameObject hitPos;
    public bool isRival;

    public void RivalIDStart()
    {
        rivalHealth = ItemData.Instance.field.rivalHealth;
    }
}
