using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using System;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public StatManager statManager = null;
    public SaveSerial serializer = null;
    public TextMeshProUGUI scoreDisplay = null;
    public TextMeshProUGUI comboDisplay = null;
    public TextMeshProUGUI accuracyDisplay = null;
    public TextMeshProUGUI wpmDisplay = null;
    private readonly string SCORE_TEXT = "Score: ";
    private readonly string COMBO_TEXT = "Combo: ";
    private readonly string WPM_TEXT = "Wpm: ";
    private readonly string ACCURACY_TEXT = "Accuracy: ";

    [SerializeField]
    GameObject gameOverScreen = null;

    public RectTransform gameOverScreenTransform = null;

    void Start()
    {
        scoreDisplay = gameOverScreen.transform.Find("ScoreTMP").GetComponent<TextMeshProUGUI>();
        comboDisplay = gameOverScreen.transform.Find("ComboTMP").GetComponent<TextMeshProUGUI>();
        accuracyDisplay = gameOverScreen.transform.Find("AccuracyTMP").GetComponent<TextMeshProUGUI>();
        wpmDisplay = gameOverScreen.transform.Find("WpmTMP").GetComponent<TextMeshProUGUI>();
        scoreDisplay.text = SCORE_TEXT + statManager.GetScore().ToString();
        comboDisplay.text = COMBO_TEXT + statManager.GetCombo().ToString();
        accuracyDisplay.text = ACCURACY_TEXT + statManager.GetAccuracy().ToString();
        wpmDisplay.text = WPM_TEXT + statManager.GetWpm().ToString();
    }

    public void ActivateGameOverScreen(bool isActivated)
    {
        if (isActivated)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            gameOverScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public bool isActive()
    {
        return gameOverScreen.activeSelf;
    }

    public void LoadGame()
    {
        MenuManager.LoadGame();
        this.gameObject.SetActive(false);
        serializer.SaveGame();
    }
    public void LoadMainMenu()
    {
        MenuManager.LoadMainMenu();
        this.gameObject.SetActive(false);
        serializer.SaveGame();
    }

    public void QuitGame()
    {
        MenuManager.QuitGame();
        this.gameObject.SetActive(false);
        serializer.SaveGame();
    }
}
