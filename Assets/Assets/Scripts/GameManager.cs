using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    void Start()
    {
        EventManager.Instance.StartListening("PlayerLost", HandlePlayerLost);
    }

    void OnDestroy()
    {
        EventManager.Instance.StopListening("PlayerLost", HandlePlayerLost);
    }

    void HandlePlayerLost()
    {
        Debug.Log("El jugador ha perdido.");
        EventManager.Instance.TriggerEvent("PlayerLost"); // Dispara el evento de perder
    }
}
