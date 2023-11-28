using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class bala : MonoBehaviour
{
    // Declarar un evento para notificar la colisión y daño
    public event Action<Collider2D, int> BulletCollisionEvent;

     private int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy") || collision.CompareTag("Muro"))
        {
            // Disparar el evento de colisión de bala y pasar el daño y el objeto de colisión
            BulletCollisionEvent?.Invoke(collision, damage);

            Destroy(gameObject);
        }
    }
}
