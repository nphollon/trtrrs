using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class HighScoreRepo {
	private List<HighScoreRecord> scores;
	private String filePath;

	public static HighScoreRepo Load(String fileName) {
		String filePath = Application.persistentDataPath + "/" + fileName;
		return new HighScoreRepo (filePath);
	}


	private HighScoreRepo(String filePath) {
		this.filePath = filePath;
		LoadScoresFromFile ();
		Sort ();
	}

	private void LoadScoresFromFile() {
		try {
			using (FileStream file = File.Open (filePath, FileMode.OpenOrCreate, FileAccess.Read)) {
				BinaryFormatter formatter = new BinaryFormatter ();
				scores = (List<HighScoreRecord>) formatter.Deserialize (file);
			}
		} catch (SerializationException) {
			scores = new List<HighScoreRecord> ();
		} catch (EndOfStreamException) {
			scores = new List<HighScoreRecord> ();
		}
	}

	private void Sort() {
		scores.Sort (HighScoreRecord.Compare);
		scores.Reverse ();
	}

	public List<HighScoreRecord> GetHighScores(int count) {
		try {
			return scores.GetRange(0, count);
		} catch (ArgumentException) {
			return scores;
		}
	}

	public int GetScore(int rank) {
		try {
			return scores [rank - 1].score;
		} catch (ArgumentOutOfRangeException) {
			return 0;
		}
	}

	public void AddScore(String name, int score) {
		scores.Add (new HighScoreRecord (name, score));
		Sort ();
		Save ();
	}
		
	private void Save() {
		using (FileStream file = File.Open (filePath, FileMode.Create, FileAccess.Write)) {
			BinaryFormatter formatter = new BinaryFormatter ();
			formatter.Serialize(file, scores);
		}
	}
}
