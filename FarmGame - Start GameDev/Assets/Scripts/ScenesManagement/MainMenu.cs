using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject controlsPanel;

    public void PlayButton(string loadScene)
    {
        SceneManager.LoadScene(loadScene);
    }

    public void InstructionsButton()
    {
        instructionsPanel.SetActive(true);
    }

    public void ControlsButton()
    {
        controlsPanel.SetActive(true);
    }

    public void GoBackToHome()
    {
        instructionsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
