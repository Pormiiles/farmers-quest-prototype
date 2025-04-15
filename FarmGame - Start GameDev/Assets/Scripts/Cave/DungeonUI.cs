using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class DungeonUI : MonoBehaviour
{
    public GameObject dungeonUIPanel;
    public TMP_Text waveText;
    public TMP_Text playerHealthText;
    public GameObject gameOverPanel;

    private Player player;
    private WaveSpawner waveSpawner;

    void Start()
    {
        player = FindObjectOfType<Player>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // Se ainda não encontrou o WaveSpawner, tenta novamente
        if (waveSpawner == null)
        {
            waveSpawner = FindObjectOfType<WaveSpawner>();
            if (waveSpawner == null) return; // Ainda não encontrou? Sai do update por agora.
        }

        if (player == null) return;

        dungeonUIPanel.SetActive(true);

        // Atualiza os textos na UI
        waveText.text = "Onda: " + (waveSpawner.currentWaveIndex + 1);
        playerHealthText.text = "Vida: " + player.currentPlayerHealth;

        // Desativa a UI assim que o Player vencer as ondas de inimigos
        if(waveSpawner.didPlayerWin == true)
        {
            dungeonUIPanel.SetActive(false);
        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

