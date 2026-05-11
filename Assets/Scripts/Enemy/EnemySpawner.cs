using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform coreTarget;
    [SerializeField] private float spawnRadius = 8f;

    [Header("Wave Settings")]
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int totalWaves = 3;

    private int currentWave = 0;
    private int aliveEnemies = 0;
    private bool waitingForWaveClear = false;

    private void Start()
    {
        GameEvents.Instance.OnEnemyDied += HandleEnemyDied;
        StartCoroutine(SpawnWaves());
    }

    private void OnDestroy()
    {
        if (GameEvents.Instance != null)
            GameEvents.Instance.OnEnemyDied -= HandleEnemyDied;
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(2f);

        while (currentWave < totalWaves)
        {
            currentWave++;
            aliveEnemies = enemiesPerWave;
            waitingForWaveClear = true;

            SpawnWave();

            // Her saniye kontrol et, hem sayaç hem sahnedeki düşman sayısı
            while (waitingForWaveClear)
            {
                int actualEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
                if (actualEnemies <= 0 && aliveEnemies <= 0)
                    waitingForWaveClear = false;

                yield return new WaitForSeconds(0.5f);
            }

            GameEvents.Instance.WaveCompleted(currentWave);
            Debug.Log("Dalga " + currentWave + " tamamlandı!");

            if (currentWave < totalWaves)
            {
                Debug.Log(timeBetweenWaves + " saniye sonra yeni dalga...");
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        GameEvents.Instance.GameOver(true);
    }

    private void SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnRadius;
            GameObject go = Instantiate(
                enemyPrefab,
                new Vector3(spawnPos.x, spawnPos.y, 0f),
                Quaternion.identity
            );

            Enemy enemy = go.GetComponent<Enemy>();
            if (enemy != null) enemy.SetTarget(coreTarget);
        }
    }

    private void HandleEnemyDied()
    {
        aliveEnemies--;
    }
}