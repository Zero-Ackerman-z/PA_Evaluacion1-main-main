using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshake : MonoBehaviour
{
    public Transform cameraTransform; // La transformaci�n de la c�mara que se sacudir�.
    public float shakeDuration = 0.2f; // Duraci�n del screenshake.
    public float shakeIntensity = 0.3f; // Intensidad del screenshake.

    private Vector3 originalPosition;
    private float shakeTimer = 0f;

    void Update()
    {
        if (shakeTimer > 0)
        {
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            shakeTimer = 0f;
            cameraTransform.localPosition = originalPosition;
        }
    }

    public void Shake()
    {
        originalPosition = cameraTransform.localPosition;
        shakeTimer = shakeDuration;
    }
}
