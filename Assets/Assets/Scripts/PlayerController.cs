using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    public HealthBarController healthBarController; 
    [SerializeField] private screenshake ScreenShake; 
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public Transform firePoint;
    public float projectileSpeed = 10f;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        Vector2 movementPlayer = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        myRBD2.velocity = movementPlayer * velocityModifier;

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckFlip(mouseInput.x);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        Debug.DrawRay(transform.position, direction * rayDistance, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Right Click");
            Shoot();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Left Click");
            Shoot();
            ScreenShake.Shake();
        }
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }

    void Shoot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        // Ajusta la rotación del sprite si es necesario
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        Vector2 bulletStartPosition = (Vector2)transform.position;

        GameObject projectile = Instantiate(projectilePrefab, bulletStartPosition, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            ScreenShake.Shake();
            TakeDamage(20);
        }
    }
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //  salud no sea menor que cero
        currentHealth = Mathf.Max(currentHealth, 0);

        // Actualizar la barra de vida del jugador 
        healthBarController.UpdateHealth(-damage);

        // Verifica si la salud es cero 
        if (currentHealth <= 0)
        {
            LoadLoseScene();
        }
    }
    
    void LoadLoseScene()
    {
        EventManager.Instance.TriggerEvent("PlayerLost"); // Dispara el evento de perder
    }
    private void OnEnable()
    {
        EventManager.Instance.StartListening("PlayerLost", () => { });
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening("PlayerLost", () => { });
    }
}
