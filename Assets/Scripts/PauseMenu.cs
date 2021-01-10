using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject gameOverScreen = null;

    [SerializeField]
    GameObject pauseMenu = null;

    // Update is called once per frame
    void Update()
    {
        // TODO: gameOVerscreen DNE
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverScreen.activeSelf)
        {
            if (isPaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        // stop time
        // bring up menu
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        MenuManager.LoadScene((int) Scenes.MainMenuScene);
        isPaused = false;
    }

    public void QuitGame()
    {
        MenuManager.QuitGame();
    }
}
