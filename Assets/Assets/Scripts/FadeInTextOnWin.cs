using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInTextOnWin : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float delayBeforeFadeIn = 2.0f; 
    [SerializeField] float fadeDuration = 2.0f;

    private bool hasAppeared = false;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; 

        StartCoroutine(StartFadeInAfterDelay());
    }

    IEnumerator StartFadeInAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeFadeIn);

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration); 
            yield return null;
        }

        hasAppeared = true;
    }

    public void KeepTextVisible()
    {
        if (!hasAppeared)
        {
            StopAllCoroutines(); // Detiene la corrutina de desvanecimiento
            canvasGroup.alpha = 1f; // Establece la transparencia al máximo para mantener el texto visible
        }
    }
}
