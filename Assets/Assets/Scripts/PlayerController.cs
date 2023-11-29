using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager; // Referencia al AudioManager
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private float lowHealthThreshold = 0.25f;
    public HealthBarController healthBarController; 
    [SerializeField] private screenshake ScreenShake; 
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField]  GameObject projectilePrefab;
    [SerializeField]  Transform firePoint;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip lowHealthEffect;

    private AudioSource audioSource;
    public float projectileSpeed = 10f;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        audioManager = FindObjectOfType<AudioManager>(); // Obtén la referencia al AudioManager en la escena
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
        if (shootSound != null && audioManager != null)
        {
            audioManager.PlayBulletSound(shootSound);
        }
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
        currentHealth = Mathf.Max(currentHealth, 0);

        healthBarController.UpdateHealth(-damage);

        if (damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Verificar si la salud es cero 
        if (currentHealth <= 0)
        {
            // Llama a la función para cargar la escena de pérdida
            LoadLoseScene();
        }
        else
        {
            // Verificar si la salud es un cuarto de su total
            if ((float)currentHealth / maxHealth <= lowHealthThreshold)
            {
                // Reproduce el efecto de vida baja si está configurado
                if (lowHealthEffect != null)
                {
                    audioSource.PlayOneShot(lowHealthEffect);
                }
            }
        }

    }

    private void OnEnable()
    {
        EventManager.Instance.StartListening("PlayerLost", LoadLoseScene);
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening("PlayerLost", LoadLoseScene);
    }

    void LoadLoseScene()
    {
        SceneManager.LoadScene("Lose");
    }
}
