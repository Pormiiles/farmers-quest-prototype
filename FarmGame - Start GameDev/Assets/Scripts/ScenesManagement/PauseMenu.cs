using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseGamePanel;

    public void pauseGameButton()
    {
        Time.timeScale = 0f;
        pauseGamePanel.SetActive(true);
    }

    public void resumeGameButton()
    {
        Time.timeScale = 1f;
        pauseGamePanel.SetActive(false);
    }

    public void backToHomeButton(string homeButton)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(homeButton);
    }
}
