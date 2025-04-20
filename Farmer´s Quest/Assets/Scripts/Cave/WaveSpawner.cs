using System.Collections;
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

    [Header("Configuração das Ondas")]
    public Wave[] waves;
    public Transform[] spawnPoints;

    [Header("Geral")]
    public int currentWaveIndex = 0;
    public GameObject dungeonGate;
    public AudioClip clip;

    [Header("Status")]
    public bool didPlayerWin;

    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    void Update()
    {
        if (!waveInProgress && enemiesAlive == 0 && currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }

        if (currentWaveIndex >= waves.Length && enemiesAlive == 0 && dungeonGate.activeSelf)
        {
            dungeonGate.SetActive(false);
            Debug.Log("Você detonou todas as ondas de monstros! Agora vá e termine de salvar o dia!");

            GameManager.instance.estadoLayla = EstadoLayla.DepoisDaCaverna;
            AudioManager.instance.playOneShotSound(clip);
            didPlayerWin = true;
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

        // Conecta eventos de morte
        Skeleton skeleton = enemy.GetComponent<Skeleton>();
        if (skeleton != null)
        {
            skeleton.onDeath += OnEnemyDeath;
        }

        Goblin goblin = enemy.GetComponent<Goblin>();
        if (goblin != null)
        {
            goblin.onDeath += OnEnemyDeath;
        }
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
