using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float damageToCore = 10f;

    [Header("Strategy")]
    [SerializeField] private bool useZigzag = false;

    private float currentHealth;
    private IMovementStrategy movementStrategy;
    private Transform coreTransform;

    public float CurrentHealth => currentHealth;
    public bool IsAlive => currentHealth > 0f;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (useZigzag)
            movementStrategy = gameObject.AddComponent<ZigzagMovement>();
        else
            movementStrategy = gameObject.AddComponent<DirectMovement>();
    }

    public void SetTarget(Transform target)
    {
        coreTransform = target;
    }

    private void Update()
    {
        if (coreTransform == null) return;
        movementStrategy.Move(transform, coreTransform, moveSpeed);
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;

        currentHealth -= amount;

        if (!IsAlive)
        {
            GameEvents.Instance.EnemyDied();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnergyCore core = other.GetComponent<EnergyCore>();
        if (core != null)
        {
            core.TakeDamage(damageToCore);
            Destroy(gameObject);
        }
    }
}