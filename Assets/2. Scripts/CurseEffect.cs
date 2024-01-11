using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseEffect : MonoBehaviour
{
    public bool isShaking = false; 

    public void StartShake()
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCamera());
        }
    }

    IEnumerator ShakeCamera()
    {
        isShaking = true;

        float duration = 1.5f; 
        float magnitude = 0.5f; 

        Vector3 originalPosition = Camera.main.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-5f, 5f) * magnitude;
            float z = originalPosition.z + Random.Range(-5f, 5f) * magnitude;

            Camera.main.transform.position = new Vector3(x, originalPosition.y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.position = originalPosition;

        isShaking = false;
    }
}