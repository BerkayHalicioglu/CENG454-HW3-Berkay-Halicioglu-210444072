using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private IWeapon weapon;
    private float nextFireTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        BaseWeapon base_weapon = GetComponent<BaseWeapon>();
        weapon = base_weapon;
    }

    private void Update()
    {
        HandleShooting();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }

    private void HandleShooting()
    {
        if (weapon == null) return;

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + weapon.FireRate;

            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;
            Vector2 direction = (mouseWorld - transform.position).normalized;

            weapon.Fire(direction, transform.position);
        }
    }

    public void ApplyUpgrade(WeaponPickup.UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case WeaponPickup.UpgradeType.RapidFire:
                weapon = new RapidFireDecorator(weapon);
                Debug.Log("Hızlı atış aktif!");
                break;
            case WeaponPickup.UpgradeType.DoubleShoot:
                weapon = new DoubleShootDecorator(weapon);
                Debug.Log("Çift mermi aktif!");
                break;
        }
    }
}