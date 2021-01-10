using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Using an average of 5 characters per word to calculate words per minute
 */
public class StatManager : MonoBehaviour
{
    private int score, combo, maxCombo, wordCount;
    private float wpm;
    private float accuracy;
    private int correctKeyStrokes, incorrectKeyStrokes, totalKeyStrokes;
    private int r1Accuracy, r2Accuracy, r3Accuracy, r4Accuracy;
    private int multiplier, multiplierLimit;
    private int COMBO_DIVISOR = 20;
    private float AVERAGE_WORD_LENGTH = 4.7f;


    public void Start()
    {
        score = 0;
        combo = 0;
        maxCombo = 0;
        multiplier = 1;
        multiplierLimit = 10;
        wordCount = 0;
        wpm = 0;
        accuracy = 0;
        r1Accuracy = 0;
        r2Accuracy = 0;
        r3Accuracy = 0;
        r4Accuracy = 0;
    }

    public float CalculateAccuracy()
    {
        if (totalKeyStrokes == 0)   // check against divide by 0.
            return 0f;

        float result = (float) correctKeyStrokes / totalKeyStrokes * 100;
        accuracy = result;
        return result;
    } 
    public float CalculateWpm()
    {
        // need to calculate word count
        // then divide by time elapsed
        int elapsedTime = (int) Time.fixedTime;

        if (elapsedTime % 2 == 0)
        {
            float averageWordCount = (float) correctKeyStrokes / AVERAGE_WORD_LENGTH;
            float minsElapsed = Time.fixedTime / 60;
            float floatWpm = averageWordCount / minsElapsed;
            wpm = floatWpm;
        }
        return wpm;
    }

    public int CalculateMaxCombo(int combo)
    {
        if (combo > maxCombo)
        {
            maxCombo = combo;
        }
        return maxCombo;
    }

    // Getters and Setters
    public void SetScore(int _score)
    {
        this.score = _score;
    }
    public int GetScore()
    {
        return this.score;
    }
    public void SetCombo(int _combo)
    {
        this.combo = _combo;
    }
    public int GetCombo()
    {
        return this.combo;
    }
    public void IncrementCombo()
    {
        this.combo++;
    }
    public int GetMaxCombo()
    {
        return this.maxCombo;
    }
    public void SetMaxCombo(int maxCombo)
    {
        this.maxCombo = maxCombo;
    }
    public int GetComboDivisor()
    {
        return COMBO_DIVISOR;
    }
    public void SetMultiplier(int multiplier)
    {
        this.multiplier = multiplier;
    }
    public int GetMultiplier()
    {
        return multiplier;
    }
    public void SetMultiplierLimit(int multiplierLimit)
    {
        this.multiplierLimit = multiplierLimit;
    }
    public int GetMultiplierLimit()
    {
        return multiplierLimit;
    }
    public void SetWpm(float wpm)
    {
        this.wpm = wpm;
    }
    public float GetWpm()
    {
        return this.wpm;
    }
    public void SetAccuracy(float accuracy)
    {
        this.accuracy = accuracy;
    }
    public float GetAccuracy()
    {
        return accuracy;
    }
    public void SetCorrectKeyStrokes(int correctKeyStrokes)
    {
        this.correctKeyStrokes = correctKeyStrokes;
    }
    public int GetCorrectKeyStrokes()
    {
        return this.correctKeyStrokes;
    }
    public void IncrementCorrectKeyStrokes()
    {
        this.correctKeyStrokes++;
    }
    public void SetIncorrectKeyStrokes(int incorrectKeyStrokes)
    {
        this.incorrectKeyStrokes = incorrectKeyStrokes;
    }
    public int GetIncorrectKeyStrokes()
    {
        return this.incorrectKeyStrokes;
    }
    public void IncrementIncorrectKeyStrokes()
    {
        this.incorrectKeyStrokes++;
    }
    public void SetTotalKeyStrokes(int totalKeyStrokes)
    {
        this.totalKeyStrokes = totalKeyStrokes;
    }
    public int GetTotalKeyStrokes()
    {
        return this.totalKeyStrokes;
    }
    public void IncrementTotalKeyStrokes()
    {
        this.totalKeyStrokes++;
    }
}
