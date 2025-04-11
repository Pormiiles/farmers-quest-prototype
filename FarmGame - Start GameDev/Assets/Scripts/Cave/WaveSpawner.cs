using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public GameObject[] enemyPrefabs;
        public int enemyCount;
        public float spawnInterval;
    }

    public Wave[] waves;
    public int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;
    public GameObject dungeonGate;

    public Transform[] spawnPoints;

    void Update()
    {
        if (!waveInProgress && enemiesAlive == 0 && currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));

        }

        if (currentWaveIndex >= waves.Length && enemiesAlive == 0 && dungeonGate.activeSelf)
        {
            dungeonGate.SetActive(false);
            Debug.Log("Você detonou todas as ondas de esqueletos! Agora vá e termine de salvar o dia!");
            GameManager.instance.estadoLayla = EstadoLayla.DepoisDaCaverna; // Muda o estado da NPC e atualiza o diálogo dela
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        waveInProgress = true;
        Debug.Log("Iniciando onda: " + wave.waveName);

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        waveInProgress = false;
        currentWaveIndex++;
    }

    void SpawnEnemy(Wave wave)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyToSpawn = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];

        GameObject enemy = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
        enemiesAlive++;

        enemy.GetComponent<Skeleton>().onDeath += OnEnemyDeath;
    }

    void OnEnemyDeath()
    {
        enemiesAlive--;
        if (currentWaveIndex >= waves.Length && enemiesAlive <= 0)
        {
            Debug.Log("Todas as ondas concluídas!"); 
        }
    }
}
