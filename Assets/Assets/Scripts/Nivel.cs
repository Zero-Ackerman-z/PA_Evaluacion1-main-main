using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Nivel : MonoBehaviour
{
    private int nivel = 1; // Nivel actual (comienza en 1)
    private int puntajeParaSiguienteNivel = 20; // Puntaje requerido para pasar al siguiente nivel
    [SerializeField] private Text nivelText;
    private score puntaje;

    void Start()
    {
        ActualizarNivel(); // Actualizar el texto del nivel al iniciar
        puntaje = FindObjectOfType<score>(); // Encuentra el script score
        puntaje.OnScoreChanged += VerificarNivel; // Suscribe el método VerificarNivel al evento OnScoreChanged
    }

    void ActualizarNivel()
    {
        nivelText.text = "Nivel: " + nivel.ToString();
    }

    void VerificarNivel(int nuevoPuntaje)
    {
        if (nuevoPuntaje >= puntajeParaSiguienteNivel * nivel)
        {
            nivel++; // Incrementar el nivel
            ActualizarNivel(); // Actualizar el texto del nivel

            if (nivel >= 3)
            {
                // Cargar la escena "Win" cuando el jugador alcance el nivel 3
                SceneManager.LoadScene("Wind");
            }

            // Lógica adicional para cambios de nivel si es necesario
        }
    }
}
