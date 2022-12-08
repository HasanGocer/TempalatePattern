using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketManager : MonoBehaviour
{
    //managerde bulunacak

    [SerializeField] private int _velocityPower;
    [SerializeField] private GameObject _angleObject;
    [SerializeField] private int _angleDownLimit, _angleUpLimit, _angleZCordinate = 30;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int _OPTrashCount;

    public void JumpObject()
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPTrashCount);
        int angleLimitWithRandom = Random.Range(_angleDownLimit, _angleUpLimit);
        obj.GetComponent<ObjectTouchPlane>().objectCount = angleLimitWithRandom;
        obj.transform.GetChild(angleLimitWithRandom).gameObject.SetActive(true);
        obj.transform.rotation = Quaternion.Euler(0, angleLimitWithRandom, _angleZCordinate);
        rb.velocity = new Vector3(0, _velocityPower, 0);
    }
}
