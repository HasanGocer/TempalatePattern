using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerID : MonoBehaviour
{
    [Header("Walker_field")]
    [Space(10)]

    public int ID;
    public bool isLive = true;
    public int pathSelection;
    public int healthCount;
    public GameObject hitPos;
    public CapsuleCollider capsuleCollider;

    [Header("Walker_Components")]
    [Space(10)]
    public CharacterBar CharacterBar;

    public void StartWalkerID(int ID, int health)
    {
        this.ID = ID;
        isLive = true;
        capsuleCollider.enabled = true;
        healthCount = health;
    }
}
