using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GhostManager : MonoSingleton<GhostManager>
{
    public GameObject mainPlayer;
    public GameObject hitPos;
    public AnimController animController;
    public Joystick joystick;
    public VolumeProfile volume;
    public int mainHealth;
    public GameObject midPos;

    public void StartGhostManager()
    {
        mainHealth = ItemData.Instance.field.mainHealth;
    }
}
