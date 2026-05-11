using UnityEngine;

public class RapidFireDecorator : WeaponDecorator
{
    private float speedMultiplier;

    public RapidFireDecorator(IWeapon weapon, float speedMultiplier = 2f)
        : base(weapon)
    {
        this.speedMultiplier = speedMultiplier;
    }

    public override float FireRate => wrapped.FireRate / speedMultiplier;
}