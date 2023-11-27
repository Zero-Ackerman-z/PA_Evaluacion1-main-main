using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class bala : MonoBehaviour
{
    // Declarar un evento para notificar la colisi�n y da�o
    public event Action<Collider2D, int> BulletCollisionEvent;

    [SerializeField] private int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy") || collision.CompareTag("Muro"))
        {
            // Disparar el evento de colisi�n de bala y pasar el da�o y el objeto de colisi�n
            BulletCollisionEvent?.Invoke(collision, damage);

            // Destruir la bala
            Destroy(gameObject);
        }
    }
}
