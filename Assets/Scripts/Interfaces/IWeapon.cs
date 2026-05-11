using UnityEngine;

public interface IWeapon
{
    void Fire(Vector2 direction, Vector3 spawnPosition);
    float FireRate { get; }
}