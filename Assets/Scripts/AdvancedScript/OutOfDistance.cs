using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfDistance : MonoBehaviour
{
    [SerializeField] private GameObject _position;

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = _position.transform.position;
    }
}
