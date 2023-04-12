using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WallTouch : MonoBehaviour
{
    [SerializeField] TMP_Text _wallText;
    [SerializeField] ItemSystem.WallStat _wallStat;
    [SerializeField] int _wallCount;
    [SerializeField] Collider _collider, _friendCollider;

    private void Start()
    {
        if (Random.Range(0, 2) == 0)
        {
            _wallStat = ItemSystem.WallStat.plus;
            _wallCount = Random.Range(3, 6);
            _wallText.text = "+ " + _wallCount;
        }
        else
        {
            _wallStat = ItemSystem.WallStat.minus;
            _wallCount = Random.Range(3, 6);
            _wallText.text = "- " + _wallCount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_wallStat == ItemSystem.WallStat.plus)
            {
                PlayerManager.Instance.PlusPlayerItemCount(_wallCount);
                _collider.enabled = false;
                _friendCollider.enabled = false;
            }
            else
            {
                PlayerManager.Instance.PlusPlayerItemCount(-_wallCount);
                _collider.enabled = false;
                _friendCollider.enabled = false;
            }
        }
    }
}
