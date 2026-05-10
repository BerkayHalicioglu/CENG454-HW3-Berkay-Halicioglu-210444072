using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        GameEvents.Instance.OnGameOver += HandleGameOver;
        GameEvents.Instance.OnCoreDied += HandleCoreDied;
    }

    private void OnDestroy()
    {
        if (GameEvents.Instance == null) return;
        GameEvents.Instance.OnGameOver -= HandleGameOver;
        GameEvents.Instance.OnCoreDied -= HandleCoreDied;
    }

    private void HandleGameOver(bool playerWon)
    {
        if (playerWon)
            Debug.Log("KAZANDIN! Tüm dalgalar temizlendi.");
        else
            Debug.Log("KAYBETTİN! Core yok edildi.");

        Invoke(nameof(ReloadScene), 3f);
    }

    private void HandleCoreDied()
    {
        GameEvents.Instance.GameOver(false);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}