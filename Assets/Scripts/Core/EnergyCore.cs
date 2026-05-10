using UnityEngine;

public class EnergyCore : MonoBehaviour, IDamageable
{
    [Header("Core Settings")]
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;

    public float CurrentHealth => currentHealth;
    public bool IsAlive => currentHealth > 0f;

    private void Start()
    {
        currentHealth = maxHealth;
        GameEvents.Instance.CoreHealthChanged(currentHealth);
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f);

        GameEvents.Instance.CoreHealthChanged(currentHealth);

        if (!IsAlive)
            GameEvents.Instance.CoreDied();
    }
}