using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Consider this file as Main
 */
public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public Timer timer = null;
    public StatManager statManager = null;
    public Text wordDisplay = null;
    public Text scoreDisplay = null;
    public Text comboDisplay = null;
    public Text timerDisplay = null;
    public Text wpmDisplay = null;
    public Text accuracyDisplay = null;
    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;
    private readonly string HIGHLIGHT_STRING_COLOR = "red";
    private readonly string COMBO_TEXT = "Combo: ";
    private readonly string WPM_TEXT = "Wpm: ";
    private readonly string ACCURACY_TEXT = "Accuracy: ";

    public GameOverScreen gameOverScreen = null;
    //public PauseMenu pauseMenu = null;
    public enum Scenes
    {
        GameScene = 0,
        MainMenuScene = 1
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentWord();
        scoreDisplay.text = "Score (1x): " + statManager.GetScore();
        comboDisplay.text = COMBO_TEXT + "0";
        wpmDisplay.text = WPM_TEXT + "0";
        accuracyDisplay.text = ACCURACY_TEXT + "0%";
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimer(timerDisplay);
        CheckInput();
        UpdateWpm();
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // do nothing
        }
        else if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            Debug.Log("keyPressed: " + keysPressed);

            // if multiple keys pressed, choose only 1
            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            } else if (keysPressed.Length > 1)
            {statManager.IncrementTotalKeyStrokes();
                EnterLetter(keysPressed[0].ToString());
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        /* Modified:
         * Remove letter
         * Score
         * Combo
         * Multiplier
         * CorrectKeystrokes
         * Word
         */
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();
            SetScore(statManager.GetScore() + 10 * statManager.GetMultiplier());
            statManager.IncrementCombo();
            statManager.IncrementCorrectKeyStrokes();
            SetCombo(statManager.GetCombo());
            statManager.CalculateMaxCombo(statManager.GetCombo());
            SetMultiplier();
            if (IsWordComplete())
            {
                SetCurrentWord();
            }
        }
        else
        {
            // Reset combo, multiplier
            // IncorrectKeyStrokes
            SetCombo(0);
            statManager.SetMultiplier(1);
            statManager.IncrementIncorrectKeyStrokes();
        }

        statManager.IncrementTotalKeyStrokes();
        float currAccuracy = statManager.CalculateAccuracy();
        accuracyDisplay.text = ACCURACY_TEXT + currAccuracy.ToString("0.00") + "%";
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }
    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }

    private void UpdateWpm()
    {
        float wpm = statManager.CalculateWpm();
        wpmDisplay.text = WPM_TEXT + wpm.ToString("0.0");
    }
    private void CheckTimer(Text timerDisplay)
    {
        if (!timer.UpdateTimerDisplay(timerDisplay))
        {
            gameOverScreen.ActivateGameOverScreen(true);
            //Debug.Log("Times up! Exiting program");
            //Application.Quit();
        }
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetNextWord();
        SetRemainingWord(currentWord);
    }

    private void SetScore(int _score)
    {
        statManager.SetScore(_score);
        scoreDisplay.text = "Score (" + statManager.GetMultiplier() + "x): " + statManager.GetScore();
    }
    public int GetScore()
    {
        return statManager.GetScore();
    }
    private void SetCombo(int _combo)
    {
        statManager.SetCombo(_combo);
        comboDisplay.text = COMBO_TEXT + statManager.GetCombo().ToString();
    }
    public int GetCombo()
    {
        return statManager.GetCombo();
    }
    /**
     * Limit the multiplier set by the multiplierLimit variable
     * Multiplier calculated by combo / COMBO_DIVISOR, int will eliminate decimal remainder
     */
    private void SetMultiplier()
    {
        if (statManager.GetMultiplier() >= statManager.GetMultiplierLimit())
        {
            statManager.SetMultiplier(statManager.GetMultiplierLimit());
        } else
        {
            statManager.SetMultiplier(1 + statManager.GetCombo() / statManager.GetComboDivisor());
        }
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordDisplay.text = remainingWord;
        if (remainingWord != null && remainingWord.Length > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<color=" + HIGHLIGHT_STRING_COLOR + ">" + remainingWord[0] + "</color>");
            sb.Append(remainingWord.Substring(1));
            wordDisplay.text = sb.ToString();
        }
    }
}


