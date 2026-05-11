using UnityEngine;

public class DoubleShootDecorator : WeaponDecorator
{
    private float spreadAngle = 15f;

    public DoubleShootDecorator(IWeapon weapon) : base(weapon) { }

    public override void Fire(Vector2 direction, Vector3 spawnPosition)
    {
        wrapped.Fire(direction, spawnPosition);

        float spreadRad = spreadAngle * Mathf.Deg2Rad;

        Vector2 spreadDir = new Vector2(
            Mathf.Cos(Mathf.Atan2(direction.y, direction.x) + spreadRad),
            Mathf.Sin(Mathf.Atan2(direction.y, direction.x) + spreadRad)
        );

        wrapped.Fire(spreadDir, spawnPosition);
    }
}