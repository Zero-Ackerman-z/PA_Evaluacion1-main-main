using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    private int score = 0;
    private Text scoreText;

    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject("ScoreManager");
                    instance = singleton.AddComponent<ScoreManager>();
                }
            }
            return instance;
        }
    }

    public int Score { get { return score; } }

    public event Action<int> OnScoreChanged;

    private void Start()
    {
        scoreText = GetComponent<Text>();
        UpdateScoreText();
    }

    public void IncreaseScore(int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Puntaje: " + Score.ToString();
    }
}
