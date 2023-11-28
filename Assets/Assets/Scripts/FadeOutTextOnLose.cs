using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutTextOnLose : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        // Aseg�rate de desactivar el texto al inicio si no quieres que sea visible al principio
        canvasGroup.alpha = 0f;
    }

    void Update()
    {
        // Verifica si la escena activa es "Lose"
        if (SceneManager.GetActiveScene().name == "Lose")
        {
            // Inicia la animaci�n de desvanecimiento
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        float timer = 0f;
        float fadeDuration = 2.0f; // Duraci�n del desvanecimiento

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration); // Desvanece el texto
            yield return null;
        }

        // Aseg�rate de que el texto est� completamente desvanecido
        canvasGroup.alpha = 0f;
    }

}
