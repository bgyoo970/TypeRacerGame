using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSerial : MonoBehaviour
{
	public StatManager statManager = null;

	/*
	 * This start method should be the main method used to swap out classes
	 * This is intentional to be semi-modular with any other
	 * classes that want to save their data
	 */
	//void Start()
 //   {
 //   }

	/*
	 * Save this session to file
	 * IF a record already exists, check to save into a list of record saves
	 * if record saves is at max saves, delete oldest save and store into file.
	 */
	public void SaveGame()
	{
		BinaryFormatter bf = new BinaryFormatter();
		// If file exists, get data from it
		if (File.Exists(Application.persistentDataPath
			  + "/scoreData.dat"))
		{
			FileStream file =
					   File.Open(Application.persistentDataPath
					   + "/scoreData.dat", FileMode.Open);
			ScoreDataContainer scoreDataContainer = (ScoreDataContainer) bf.Deserialize(file);
			file.Close();
			ScoreData data = new ScoreData();
			data.Score = statManager.GetScore();
			data.MaxCombo = statManager.GetMaxCombo();
			data.Wpm = statManager.GetWpm();
			data.Accuracy = statManager.GetAccuracy();
			data.Error = statManager.GetIncorrectKeyStrokes();
			scoreDataContainer.Add(data);

			FileStream newFile =
					   File.Create(Application.persistentDataPath
					   + "/scoreData.dat");
			bf.Serialize(newFile, scoreDataContainer);
			newFile.Close();
        }
		// If file DNE exists, create file and store data
        else
        {
			FileStream file = File.Create(Application.persistentDataPath 
				+ "/scoreData.dat");
			ScoreData data = new ScoreData();
			data.Score = statManager.GetScore();
			data.MaxCombo = statManager.GetMaxCombo();
			data.Wpm = statManager.GetWpm();
			data.Accuracy = statManager.GetAccuracy();
			data.Error = statManager.GetIncorrectKeyStrokes();

			ScoreDataContainer scoreDataContainer = new ScoreDataContainer();
			scoreDataContainer.Add(data);

			bf.Serialize(file, scoreDataContainer);
            file.Close();
        }
        Debug.Log("Game data saved!");
    }

	public void LoadGame()
	{
		if (File.Exists(Application.persistentDataPath
					   + "/scoreData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(Application.persistentDataPath
                       + "/scoreData.dat", FileMode.Open);

			ScoreDataContainer scoreDataContainer = (ScoreDataContainer) bf.Deserialize(file);

			foreach (ScoreData scoreData in scoreDataContainer.GetScoreDataList())
            {
				Debug.Log("scoreData.score: " + scoreData.Score);
				Debug.Log("scoreData.combo: " + scoreData.MaxCombo);
				Debug.Log("scoreData.wpm: " + scoreData.Wpm);
				Debug.Log("scoreData.acc: " + scoreData.Accuracy);
				Debug.Log("scoreData.error: " + scoreData.Error);
				Debug.Log("------: " + scoreData.Error);
			}
			
            file.Close();
            Debug.Log("Game data loaded!");
		}
		else
			Debug.LogError("There is no save data!");
	}

	public ScoreDataContainer LoadSaveData()
    {
		ScoreDataContainer scoreDataContainer = null;
		if (File.Exists(Application.persistentDataPath
					   + "/scoreData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file =
					   File.Open(Application.persistentDataPath
					   + "/scoreData.dat", FileMode.Open);

			scoreDataContainer = (ScoreDataContainer)bf.Deserialize(file);
			file.Close();
			Debug.Log("Game data loaded!");
		}
		else
		{
			Debug.LogError("There is no save data!");
		}

		return scoreDataContainer;
	}

	void ResetData()
	{
		if (File.Exists(Application.persistentDataPath
					  + "/scoreData.dat"))
		{
			File.Delete(Application.persistentDataPath
							  + "/scoreData.dat");
			Debug.Log("Data reset complete!");
		}
		else
			Debug.LogError("No save data to delete.");
	}

	[Serializable]
	public class ScoreDataContainer
	{
		private List<ScoreData> scoreDataList;

		public ScoreDataContainer()
        {
			scoreDataList = new List<ScoreData>();
		}
		public void Add(ScoreData scoreData)
        {
			int listSize = scoreDataList.Count;
			while (listSize >= 10)
            {
				scoreDataList.RemoveAt(0);
				listSize--;
            }
            scoreDataList.Add(scoreData);
        }
		public List<ScoreData> GetScoreDataList()
        {
			return scoreDataList;
        }
	}

	[Serializable]
	public class ScoreData
	{
		private int score, maxCombo, error;
		private float wpm, accuracy;

        public int Score { get => score; set => score = value; }
        public int MaxCombo { get => maxCombo; set => maxCombo = value; }
        public float Wpm { get => wpm; set => wpm = value; }
        public float Accuracy { get => accuracy; set => accuracy = value; }
        public int Error { get => error; set => error = value; }
	}
}
