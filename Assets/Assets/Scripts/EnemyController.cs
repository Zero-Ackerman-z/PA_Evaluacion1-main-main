using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D detectionZone;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float returnSpeed = 3f;
    [SerializeField] private AudioClip pursuitSound;
    [SerializeField] private AudioClip backgroundMusic;

    private Transform player;
    private Vector3 initialPosition;
    private bool isChasing = false;
    private AudioManager audioManager; 

    private AudioSource audioSource;
    private bool isBackgroundMusicPlaying = false;

    private void Start()
    {
        initialPosition = transform.position;
        audioSource = GetComponent<AudioSource>(); 
        audioManager = FindObjectOfType<AudioManager>();

    }

    private void Update()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                return; // Si no encuentra al jugador, no hace nada más en este frame.
            }
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float detectionRadius = detectionZone.radius * transform.lossyScale.x;

        if (distanceToPlayer < detectionRadius)
        {
            isChasing = true;

            // Reproducir sonido de persecución cuando entra en modo persecución
            if (pursuitSound != null && !audioSource.isPlaying)
            {
                audioSource.clip = pursuitSound;
                audioSource.Play();
            }

            // Si la música de fondo está sonando, la silenciamos
            if (isBackgroundMusicPlaying)
            {
                isBackgroundMusicPlaying = false;
                audioManager.MuteChannel(audioManager.musicChannelConfig, true);
            }
        }
        else
        {
            isChasing = false;

            // Si la música de persecución está sonando, la silenciamos
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // Si la música de fondo no está sonando, la activamos
            if (!isBackgroundMusicPlaying)
            {
                isBackgroundMusicPlaying = true;
                audioManager.MuteChannel(audioManager.musicChannelConfig, false);
                audioManager.PlayBackgroundMusic(backgroundMusic); 
            }
        }

        if (isChasing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, initialPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, returnSpeed * Time.deltaTime);
        }
    }

}
