using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deteccionBala : MonoBehaviour
{
    [SerializeField] public ScoreManager scoreManager;
    private int maxHealth = 100;
    private int currentHealth;
    [SerializeField] public HealthBarController healthBarController; 
    private void Start()
    {
        currentHealth = maxHealth;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bala"))
        {
            TakeDamage(20);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            scoreManager.IncreaseScore(20); 
        }
        healthBarController.UpdateHealth(-damage);

    }
}