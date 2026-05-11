using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float lifetime = 3f;

    private Vector2 direction;
    private float spawnTime;

    public void Init(Vector2 dir)
    {
        direction = dir;
        spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - spawnTime >= lifetime)
        {
            ProjectilePool.Instance.ReturnToPool(gameObject);
            return;
        }

        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null)
        {
            target.TakeDamage(damage);
            ProjectilePool.Instance.ReturnToPool(gameObject);
        }
    }

    public void OnSpawnFromPool()
    {
        spawnTime = Time.time;
    }

    public void OnReturnToPool()
    {
        direction = Vector2.zero;
    }
}