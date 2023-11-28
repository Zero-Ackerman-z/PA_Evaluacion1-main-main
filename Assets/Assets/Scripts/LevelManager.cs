using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    private static LevelManager instance; // Instancia Singleton

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>(); // Intenta encontrar una instancia existente en la escena
            }
            return instance;
        }
    }

    private int nivel = 1; // Nivel actual 
    private int puntajeParaSiguienteNivel = 20; // Puntaje requerido 
    [SerializeField] private Text nivelText;
    private ScoreManager puntaje; 

    private void Awake()
    {
        // Si ya hay una instancia diferente, destruye esta
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Si no hay ninguna instancia, establece esta como la instancia actual
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Encuentra el script de Score
        puntaje = FindObjectOfType<ScoreManager>();

        // Suscribe el método VerificarNivel al evento OnScoreChanged
        puntaje.OnScoreChanged += VerificarNivel;

        ActualizarNivel();
    }

    void ActualizarNivel()
    {
        nivelText.text = "Nivel: " + nivel.ToString();
    }

    void VerificarNivel(int nuevoPuntaje)
    {
        if (nuevoPuntaje >= puntajeParaSiguienteNivel * nivel)
        {
            nivel++; 
            ActualizarNivel(); 

            if (nivel >= 3)
            {
                SceneManager.LoadScene("Wind");
            }

        }
    }
}
