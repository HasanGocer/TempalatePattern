using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SweetLerp : MonoBehaviour
{
    public Image bar;
    [SerializeField] float Amount = 1, timerSpeed = 1;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(BarUpdate());
        }
    }

    IEnumerator BarUpdate()
    {
        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime * timerSpeed;
            Debug.Log(timer);
            Amount = Mathf.Lerp(1, 0, timer);
            bar.GetComponent<Image>().fillAmount = Amount;
            yield return new WaitForEndOfFrame();
            if (Amount == 0)
            {
                break;
            }
        }
    }
}
