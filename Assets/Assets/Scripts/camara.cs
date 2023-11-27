using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camara : MonoBehaviour
{
     [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        // Encuentra el componente CinemachineVirtualCamera en la escena.
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Detén el seguimiento de la cámara al jugador deshabilitando el componente.
            virtualCamera.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reanuda el seguimiento de la cámara al jugador habilitando nuevamente el componente.
            virtualCamera.enabled = true;
        }
    }
}
