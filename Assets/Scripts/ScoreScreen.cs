using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SaveSerial;

public class ScoreScreen : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    public SaveSerial serializer = null;

    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        ScoreDataContainer scoreDataContainer = serializer.LoadSaveData();
        if (scoreDataContainer == null) // null check
            return;

        //foreach (ScoreData scoreData in scoreDataContainer.GetScoreDataList())
        //{
        //    Debug.Log("scoreData.score: " + scoreData.Score);
        //    Debug.Log("scoreData.combo: " + scoreData.MaxCombo);
        //    Debug.Log("scoreData.wpm: " + scoreData.Wpm);
        //    Debug.Log("scoreData.acc: " + scoreData.Accuracy);
        //    Debug.Log("scoreData.error: " + scoreData.Error);
        //    Debug.Log("------");
        //}

        List<ScoreData> scoreDataList = scoreDataContainer.GetScoreDataList();
        float templateHeight = 45f;
        for (int i = scoreDataList.Count - 1, j = 0; i >= 0; i--)
            //for (int i = 0; i < scoreDataList.Count; i++)
        {
            ScoreData currScoreData = scoreDataList[i];
            Debug.Log("currScoreData.Score.ToString(): " + currScoreData.Score.ToString());

            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(entryRectTransform.position.x, -templateHeight * j);
            j++;

            entryTransform.Find("ScoreText").GetComponent<Text>().text = currScoreData.Score.ToString();
            entryTransform.Find("MaxComboText").GetComponent<Text>().text = currScoreData.MaxCombo.ToString();
            entryTransform.Find("WPMText").GetComponent<Text>().text = currScoreData.Wpm.ToString("0.0");
            entryTransform.Find("AccuracyText").GetComponent<Text>().text = currScoreData.Accuracy.ToString("0.00") + "%";
            entryTransform.Find("ErrorsText").GetComponent<Text>().text = currScoreData.Error.ToString();
            entryTransform.gameObject.SetActive(true);
        }
    }
}
