using UnityEngine;

public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon wrapped;

    public WeaponDecorator(IWeapon weapon)
    {
        wrapped = weapon;
    }

    public virtual float FireRate => wrapped.FireRate;

    public virtual void Fire(Vector2 direction, Vector3 spawnPosition)
    {
        wrapped.Fire(direction, spawnPosition);
    }
}