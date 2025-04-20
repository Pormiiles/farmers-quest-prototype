using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DungeonUI : MonoBehaviour
{
    [Header("Painéis")]
    public GameObject dungeonUIPanel;
    public GameObject gameOverPanel;

    [Header("Textos e Barras")]
    public TMP_Text waveText;
    public Image playerHealthBarUI;

    [Header("HUD")]
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
        if (waveSpawner == null)
        {
            waveSpawner = FindObjectOfType<WaveSpawner>();
            if (waveSpawner == null) return;
        }

        if (player == null) return;

        dungeonUIPanel.SetActive(true);

        waveText.text = "Onda: " + (waveSpawner.currentWaveIndex + 1);
        playerHealthBarUI.fillAmount = player.currentPlayerHealth / player.totalPlayerHealth;

        if (waveSpawner.didPlayerWin)
        {
            dungeonUIPanel.SetActive(false);
        }

        if (player.isPlayerDead)
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
