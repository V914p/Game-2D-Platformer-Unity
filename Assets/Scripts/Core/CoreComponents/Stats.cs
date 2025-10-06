using System;
using UnityEngine;

public class Stats : CoreComponent
{
    public event Action OnHealthZero;
    public event Action<float, float> OnHealthChanged; // báo cho UI

    [SerializeField] private float maxHealth = 5f; // Enemy1 = 5, Enemy2 = 3
    [SerializeField] private float currentHealth;

    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnHealthZero?.Invoke();
            Debug.Log($"{gameObject.name} đã chết!!");
        }

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
}
