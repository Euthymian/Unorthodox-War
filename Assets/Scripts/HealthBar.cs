using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private Transform healthBar;

    private void Awake()
    {
        healthSystem = GetComponentInParent<HealthSystem>();
    }

    private void OnEnable()
    {
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void OnDisable()
    {
        healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(float healthNormalized)
    {
        UpdateHealthBar(healthNormalized);
    }

    private void UpdateHealthBar(float healthNormalized)
    {
        healthBar.localScale = new Vector3(healthNormalized, 1, 1);
    }
}