using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class DungeonUI : MonoBehaviour
{
    public GameObject dungeonUIPanel;
    public TMP_Text waveText;
    public Image playerHealthBarUI;
    public GameObject gameOverPanel;
    public GameObject hudCollectable;
    public GameObject hudTools;

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
        playerHealthBarUI.fillAmount = player.currentPlayerHealth / player.totalPlayerHealth;

        // Desativa a UI assim que o Player vencer as ondas de inimigos
        if(waveSpawner.didPlayerWin == true)
        {
            dungeonUIPanel.SetActive(false);
        }

        if(player.isPlayerDead)
        {
            hudCollectable.SetActive(false);
            hudTools.SetActive(false);
        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

