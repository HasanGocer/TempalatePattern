using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoSingleton<CameraShake>
{
    [SerializeField] float duration, magnitude, minRange, maxRange;

    public IEnumerator CameraShakes(float duration, float magnitude, float minRange, float maxRange)
    {
        Vector3 originalPosition = transform.position;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-minRange, maxRange) * magnitude;
            float y = Random.Range(-minRange, maxRange) * magnitude;

            transform.position = new Vector3(x, originalPosition.y, transform.position.z); ;

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }

    public void CameraShakeCall()
    {
        StartCoroutine(CameraShakes(duration, magnitude, minRange, maxRange));
    }
}
