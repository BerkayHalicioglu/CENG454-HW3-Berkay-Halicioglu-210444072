using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI coreHealthText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;

    [Header("Settings")]
    [SerializeField] private int totalWaves = 3;
    [SerializeField] private float maxCoreHealth = 100f;

    private void Start()
    {
        GameEvents.Instance.OnCoreHealthChanged += UpdateCoreHealth;
        GameEvents.Instance.OnWaveCompleted     += UpdateWave;
        GameEvents.Instance.OnGameOver          += ShowGameOver;

        gameOverPanel.SetActive(false);
        UpdateCoreHealth(maxCoreHealth);
        UpdateWave(0);
    }

    private void OnDestroy()
    {
        if (GameEvents.Instance == null) return;
        GameEvents.Instance.OnCoreHealthChanged -= UpdateCoreHealth;
        GameEvents.Instance.OnWaveCompleted     -= UpdateWave;
        GameEvents.Instance.OnGameOver          -= ShowGameOver;
    }

    private void UpdateCoreHealth(float newHealth)
    {
        coreHealthText.text = "Core: " + Mathf.CeilToInt(newHealth);
    }

    private void UpdateWave(int completedWave)
    {
        int currentWave = completedWave + 1;
        if (currentWave > totalWaves) currentWave = totalWaves;
        waveText.text = "Wave: " + currentWave + " / " + totalWaves;
    }

    private void ShowGameOver(bool playerWon)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = playerWon ? "KAZANDIN!" : "KAYBETTİN!";
    }
}