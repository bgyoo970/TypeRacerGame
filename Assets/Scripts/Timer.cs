using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timeRemaining;
    private float currMs, currSeconds, currMins;
    private float TIME_LIMIT = 60.00f;

    public void Start()
    {
        timeRemaining = TIME_LIMIT;
        currMs = 0;
        currSeconds = 0;
        currMins = 0;
    }

    /**
     * Returns success if timeRemaining still is greater than 0
     */
    public bool UpdateTimerDisplay(Text timerDisplay)
    {
        // decreasing timer - only update when timer is greater than 0
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            currMs = GetCurrMillis();
            currSeconds = GetCurrSeconds();
            currMins = GetCurrMins();

            timerDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", currMins, currSeconds, currMs);
        }
        else
        {
            return false;
        }

        return true;
    }

    public float GetCurrMillis()
    {
        currMs = (int)((timeRemaining - (int)timeRemaining) * 100);
        return currMs;
    }
    public float GetCurrSeconds()
    {
        currSeconds = (int)(timeRemaining % 60);
        return currSeconds;
    }
    public float GetCurrMins()
    {
        currMins = (int)(timeRemaining / 60 % 60);
        return currMins;
    }
}
