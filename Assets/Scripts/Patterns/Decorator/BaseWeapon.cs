using UnityEngine;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private float fireRate = 0.3f;
    public float FireRate => fireRate;

    public void Fire(Vector2 direction, Vector3 spawnPosition)
    {
        GameObject bullet = ProjectilePool.Instance.Get(
            spawnPosition,
            Quaternion.Euler(0f, 0f,
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)
        );

        Projectile p = bullet.GetComponent<Projectile>();
        if (p != null) p.Init(direction);
    }
}