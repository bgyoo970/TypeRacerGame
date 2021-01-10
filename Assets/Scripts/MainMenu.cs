using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        MenuManager.LoadScene((int) Scenes.GameScene);
    }

    public void QuitGame()
    {
        MenuManager.QuitGame();
    }
}
