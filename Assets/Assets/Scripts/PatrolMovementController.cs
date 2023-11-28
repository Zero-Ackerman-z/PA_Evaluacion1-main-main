using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private float raycastLength = 2f;
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float normalVelocity = 3f;
    [SerializeField] private float chaseVelocity = 5f;
    [SerializeField] public Transform player;
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    private bool isChasing = false;
    [SerializeField] public ScoreManager scoreManager;
    [SerializeField] private HealthBarController healthBarController; // Referencia al HealthBarController
    private int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
        scoreManager = FindObjectOfType<ScoreManager>();
        currentHealth = maxHealth;

    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < 3.0f)
        {
            isChasing = true;
            myRBD2.velocity = (player.position - transform.position).normalized * chaseVelocity;
        }
        else
        {
            isChasing = false;
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * normalVelocity;
        }

        // Dibuja el Raycast en la dirección de movimiento.
        Debug.DrawRay(transform.position, myRBD2.velocity.normalized * raycastLength, Color.red);

        // Verifica si el enemigo llegó al punto de patrulla y ajusta el giro en el eje X.
        if (Vector2.Distance(transform.position, currentPositionTarget.position) < 0.1f)
        {
            patrolPos = (patrolPos + 1) % checkpointsPatrol.Length;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            CheckFlip();
        }
    }

    private void CheckFlip()
    {
        spriteRenderer.flipX = (currentPositionTarget.position.x - transform.position.x) < 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            TakeDamage(20);

            if (currentHealth <= 0)
            {
                // Aumenta la puntuación en 20 puntos al destruir al enemigo
                scoreManager.IncreaseScore(20);

                // Destruye al enemigo
                Destroy(gameObject);
            }

            // Destruye la bala
            Destroy(collision.gameObject);
        }
    }
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // Asegura que la salud no sea menor que cero
        currentHealth = Mathf.Max(currentHealth, 0);

        // Actualizar la barra de vida del enemigo en el HealthBarController
        healthBarController.UpdateHealth(-damage);
    }
}
