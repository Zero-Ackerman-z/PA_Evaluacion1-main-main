using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthBarController : MonoBehaviour
{
    private static HealthBarController instance; // Instancia estática del Singleton

    [SerializeField] private int maxValue;
    [Header("Health Bar Visual Components")]
    [SerializeField] private RectTransform healthBar;
    [SerializeField] private RectTransform modifiedBar;
    [SerializeField] private float changeSpeed;
    public bool isPlayer; 

    private int currentValue;
    private float _fullWidth;
    private float TargetWidth => currentValue * _fullWidth / maxValue;
    private Coroutine updateHealthBarCoroutine;
    private int amount;

    public static HealthBarController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HealthBarController>();
                if (instance == null)
                {
                    Debug.LogError("No se encontró HealthBarController en la escena.");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Asegura que solo exista una instancia del Singleton
        if (HealthBarController.Instance != null)
        {
            HealthBarController.Instance.UpdateHealth(amount);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _fullWidth = healthBar.rect.width;
        currentValue = maxValue;
    }

    public void UpdateHealth(int amount)
    {
        currentValue = Mathf.Clamp(currentValue + amount, 0, maxValue);

        if (updateHealthBarCoroutine != null)
        {
            StopCoroutine(updateHealthBarCoroutine);
        }
        updateHealthBarCoroutine = StartCoroutine(AdjustWidthBar(amount));
    }

    private IEnumerator AdjustWidthBar(int amount)
    {
        RectTransform targetBar = amount >= 0 ? modifiedBar : healthBar;
        RectTransform animatedBar = amount >= 0 ? healthBar : modifiedBar;

        targetBar.sizeDelta = SetWidth(targetBar, TargetWidth);

        while (Mathf.Abs(targetBar.rect.width - animatedBar.rect.width) > 1f)
        {
            animatedBar.sizeDelta = SetWidth(animatedBar, Mathf.Lerp(animatedBar.rect.width, TargetWidth, Time.deltaTime * changeSpeed));
            yield return null;
        }

        animatedBar.sizeDelta = SetWidth(animatedBar, TargetWidth);
    }

    private Vector2 SetWidth(RectTransform t, float width)
    {
        return new Vector2(width, t.rect.height);
    }

    private void Update()
    {
        if (isPlayer)
        {
            // Solo el jugador puede actualizar su barra de vida
            if (Input.GetMouseButtonDown(0))
            {
            }
            else if (Input.GetMouseButtonDown(1))
            {
            }
        }
    }
}