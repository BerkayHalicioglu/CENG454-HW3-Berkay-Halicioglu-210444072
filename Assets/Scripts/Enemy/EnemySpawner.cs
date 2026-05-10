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
            SpawnWave();

            yield return new WaitUntil(() => aliveEnemies <= 0);

            GameEvents.Instance.WaveCompleted(currentWave);

            if (currentWave < totalWaves)
                yield return new WaitForSeconds(timeBetweenWaves);
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