using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperTouch : MonoSingleton<UpperTouch>
{
    [SerializeField] bool isJump;

    private void OnTriggerEnter(Collider other)
    {
        if (isJump)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            PlayerController.Instance.isFree = true;
            rb.velocity += new Vector3(0, 45, PlayerManager.Instance.ReturnPlayerItemCount());
        }
        else
            PlayerController.Instance.isFree = false;
    }
}
