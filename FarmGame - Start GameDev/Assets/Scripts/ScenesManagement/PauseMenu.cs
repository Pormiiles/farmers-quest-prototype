using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseGamePanel;
    public AudioClip openUI;
    public AudioClip closeUI;

    public void pauseGameButton()
    {
        Time.timeScale = 0f;
        AudioManager.instance.playOneShotSound(openUI);
        pauseGamePanel.SetActive(true);
    }

    public void resumeGameButton()
    {
        Time.timeScale = 1f;
        AudioManager.instance.playOneShotSound(closeUI);
        pauseGamePanel.SetActive(false);
    }

    public void backToHomeButton(string homeButton)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(homeButton);
    }
}
