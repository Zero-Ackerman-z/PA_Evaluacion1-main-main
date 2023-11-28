using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshake : MonoBehaviour
{
    public Transform cameraTransform; 
    public float shakeDuration = 0.2f; 
    public float shakeIntensity = 0.3f;

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
