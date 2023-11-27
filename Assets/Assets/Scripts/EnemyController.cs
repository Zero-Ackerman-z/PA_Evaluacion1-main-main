using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D detectionZone;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float returnSpeed = 3f;

    private Transform player;
    private Vector3 initialPosition;
    private bool isChasing = false;

    private void Start()
    {
        initialPosition = transform.position;
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
        }
        else
        {
            isChasing = false;
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
