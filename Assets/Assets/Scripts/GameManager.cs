using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public void ModifyHealth(int amount)
    {
        if (HealthBarController.Instance != null)
        {
            HealthBarController.Instance.UpdateHealth(amount);
        }
    }
    void HandlePlayerLost()
    {
        Debug.Log("El jugador ha perdido.");
        SceneManager.LoadScene("Lose");
    }
}
