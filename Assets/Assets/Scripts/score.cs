using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class score : MonoBehaviour
{
    private int Score1 = 0; // Puntaje actual
    private Text scoreText;

    public event Action<int> OnScoreChanged; // Evento que notificará cuando cambie el puntaje

    private void Start()
    {
        scoreText = GetComponent<Text>();
        UpdateScoreText();

    }

    // Implementa una propiedad para acceder al puntaje actual desde otros scripts
    public int Score
    {
        get { return Score1; }
    }

    // Método para aumentar el puntaje
    public void IncreaseScore(int points)
    {
        Score1 += points;

        // Notifica a través del evento que el puntaje ha cambiado
        OnScoreChanged?.Invoke(Score1);

        // Actualiza el texto del puntaje
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // Actualiza el texto del puntaje con el nuevo valor
        scoreText.text = "Puntaje: " + Score1.ToString();
    }
}
