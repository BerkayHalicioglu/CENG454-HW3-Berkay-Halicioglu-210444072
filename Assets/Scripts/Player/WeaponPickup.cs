using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public enum UpgradeType { RapidFire, DoubleShoot }

    [SerializeField] private UpgradeType upgradeType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ApplyUpgrade(upgradeType);
            Destroy(gameObject);
        }
    }
}