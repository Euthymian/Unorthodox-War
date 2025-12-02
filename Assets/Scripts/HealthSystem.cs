using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action<float> OnHealthChanged;
    public event Action OnHealthDepleted;

    public float maxHealth = 100;
    public float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(GetCurrentHealthNormalized());
        if (currentHealth <= 0)
        {
            OnHealthDepleted?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(GetCurrentHealthNormalized());
    }

    private float GetCurrentHealthNormalized()
    {
        return currentHealth / maxHealth;
    }

    private float GetCurrentHealth()
    {
        return currentHealth;
    }

    [ContextMenu("Heal 10")]
    public void Heal10()
    {
        Heal(10);
    }

    [ContextMenu("Damage 10")]
    public void Damage10()
    {
        TakeDamage(10);
    }
}
