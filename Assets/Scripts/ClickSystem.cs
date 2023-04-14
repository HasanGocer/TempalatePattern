using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSystem : MonoSingleton<ClickSystem>
{
    [SerializeField] GameObject _tapImage;
    bool isFirstTap;

    public void StartClick()
    {
        StartCoroutine(FirstTap());
        isFirstTap = false;
    }

    private IEnumerator FirstTap()
    {
        yield return new WaitForSeconds(10);
        if (!isFirstTap) _tapImage.SetActive(true);
    }

    public void ClickTime()
    {
        isFirstTap = true;
        _tapImage.SetActive(false);
    }
}
