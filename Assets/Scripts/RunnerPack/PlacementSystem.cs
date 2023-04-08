using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoSingleton<PlacementSystem>
{
    [SerializeField] public List<GameObject> _itemObjects = new List<GameObject>();
    [SerializeField] public List<GameObject> _itemObjectPoses = new List<GameObject>();
    [SerializeField] public List<GameObject> _moneyObjects = new List<GameObject>();
    [SerializeField] public List<GameObject> _moneyObjectPoses = new List<GameObject>();

    public void ItemPlacement()
    {
        foreach (GameObject item in _itemObjectPoses) Instantiate(_itemObjects[Random.Range(0, _itemObjects.Count)], item.transform.position, item.transform.rotation);
    }
    public void MoneyPlacement()
    {
        foreach (GameObject item in _moneyObjectPoses) Instantiate(_moneyObjects[Random.Range(0, _moneyObjects.Count)], item.transform.position, item.transform.rotation);
    }
}
