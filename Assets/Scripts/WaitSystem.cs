using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitSystem : MonoSingleton<WaitSystem>
{
    //place de bulunacak

    public int placeCount;
    public bool inPlace;
    [SerializeField] private float _timerSpeed;
    [SerializeField] private Image _barImage;

    private void OnTriggerExit(Collider other)
    {
        inPlace = false;
    }


    public IEnumerator bar(GameObject player)
    {
        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime * _timerSpeed;
            _barImage.fillAmount = Mathf.Lerp(1, 0, timer);
            yield return new WaitForEndOfFrame();
            if (_barImage.fillAmount == 0)
            {
                inPlace = true;
                StackSystem.Instance.StackDrop(this.gameObject);
                break;
            }
        }
    }
}
