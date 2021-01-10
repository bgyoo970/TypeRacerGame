using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    MainMenuScene = 0,
    GameScene = 1
}

public class MenuManager : MonoBehaviour
{
    public GameObject currMenu = null;
    public GameObject nextMenu = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currMenu.gameObject.SetActive(false);
            nextMenu.gameObject.SetActive(true);
        }
    }
    public static void LoadGame()
    {
        SceneManager.LoadScene((int) Scenes.GameScene);
    }
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene((int) Scenes.MainMenuScene);
    }
    public static void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public static void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
