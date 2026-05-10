using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Core hasar aldığında
    public event Action<float> OnCoreHealthChanged;
    // Core yok edildiğinde
    public event Action OnCoreDied;
    // Düşman öldüğünde
    public event Action OnEnemyDied;
    // Dalga tamamlandığında
    public event Action<int> OnWaveCompleted;
    // Oyun bitti
    public event Action<bool> OnGameOver; // true = kazandı

    public void CoreHealthChanged(float newHealth) =>
        OnCoreHealthChanged?.Invoke(newHealth);

    public void CoreDied() =>
        OnCoreDied?.Invoke();

    public void EnemyDied() =>
        OnEnemyDied?.Invoke();

    public void WaveCompleted(int waveNumber) =>
        OnWaveCompleted?.Invoke(waveNumber);

    public void GameOver(bool playerWon) =>
        OnGameOver?.Invoke(playerWon);
}