using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSController : MonoBehaviour
{
    float deltaTime = 0.0f;
    public TMP_Text fpsText;


    //FPS Gösteren Kod istersek ayarlardan on off diye açýp kaparýz. Kod GPT'den çýkma.
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Round(fps).ToString() + " FPS";
    }
}