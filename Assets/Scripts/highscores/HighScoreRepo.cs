using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
		String fileContents = ReadFileContents (filePath);
		scores = ParseScores (fileContents);
	}

	private List<HighScoreRecord> ParseScores(String fileContents) {
		try {
				
			String pattern = @"(\d+) ([A-Z]{3})";
			List<HighScoreRecord> scores = new List<HighScoreRecord> ();

			foreach (Match m in Regex.Matches(fileContents, pattern)) {
				int score = Int32.Parse (m.Groups [1].Value);
				String playerName = m.Groups [2].Value;
				scores.Add (new HighScoreRecord (playerName, score));
			}

			return scores;

		} catch (FormatException) {
			return new List<HighScoreRecord> ();
		}
	}


	private String ReadFileContents(String fileName) {
		try {
			using (StreamReader inputFile = new StreamReader (fileName)) {
				return inputFile.ReadToEnd ();
			}
		} catch (Exception) {
			return "";
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

	private void Sort() {
		scores.Sort (HighScoreRecord.Compare);
		scores.Reverse ();
	}

	private void Save() {
		try {
			using (StreamWriter outputFile = new StreamWriter (filePath, false)) {
				WriteScoresToStream(outputFile);
			}
		} catch (Exception) {
		}
	}

	private void WriteScoresToStream(TextWriter writer) {
		foreach(HighScoreRecord score in scores) {
			writer.WriteLine (FormatScoreRecord(score));
		}
	}

	public String FormatHighScores(int count) {
		StringBuilder builder = new StringBuilder ();
		List<HighScoreRecord> highScores = GetHighScores (count);

		foreach (HighScoreRecord score in highScores) {
			builder.AppendLine (FormatScoreRecord(score));
		}

		return builder.ToString ();
	}

	private List<HighScoreRecord> GetHighScores(int count) {
		try {
			return scores.GetRange(0, count);
		} catch (ArgumentException) {
			return scores;
		}
	}

	private String FormatScoreRecord(HighScoreRecord score) {
		return String.Format ("{0} {1}", score.score, score.name);
	}
}
