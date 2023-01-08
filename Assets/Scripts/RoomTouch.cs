using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTouch : MonoBehaviour
{
    [SerializeField] private RoomID roomID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Main"))
            TouchMain();
    }

    private void TouchMain()
    {
        foreach (int i in GhostManager.Instance.StayRoom.FriendRoom)
        {
            FinishSystem.Instance.focusScene.Rooms[i - 1].GetComponent<RoomID>().RoomActive = false;
        }
        foreach (int i in roomID.FriendRoom)
        {
            FinishSystem.Instance.focusScene.Rooms[i - 1].GetComponent<RoomID>().RoomActive = true;
        }
    }
}
