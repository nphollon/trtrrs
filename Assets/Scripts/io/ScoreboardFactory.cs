﻿using System;
using UnityEngine;
using UnityEngine.UI;

public interface Scoreboard {
	void Record (int rowsCleared);
	int GetScore ();
	int GetLevel ();
}

public class ScoreboardFactory : MonoBehaviour {
	public int[] scoreMultipliers;
	public AudioPlayer audioPlayer;
	public Text scoreText;

	public Scoreboard StartAtLevel(int startingLevel) {
		return new ScoreboardWithAudio (scoreMultipliers, startingLevel, audioPlayer, scoreText);
	}
}

class ScoreboardWithAudio : Scoreboard {
	private int score;
	private int level;
	private int rowsUntilNextLevel;

	private int[] scoreMultipliers;
	private AudioPlayer audioPlayer;
	private Text display;

	public ScoreboardWithAudio(int[] scoreMultipliers, int startingLevel, AudioPlayer audioPlayer, Text display) {
		score = 0;
		level = startingLevel;
		rowsUntilNextLevel = startingLevel * 10 + 10;

		this.scoreMultipliers = scoreMultipliers;
		this.audioPlayer = audioPlayer;
		this.display = display;

		RefreshScoreAndLevel();
	}

	public void Record(int rowsCleared) {
		UpdateScore (rowsCleared);
		UpdateLevel (rowsCleared);
		RefreshScoreAndLevel ();
	}

	private void UpdateScore(int rowsCleared) {
		audioPlayer.LandBlock (rowsCleared);
		score = score + level * scoreMultipliers [rowsCleared];
	}

	private void UpdateLevel(int rowsCleared) {
		rowsUntilNextLevel = rowsUntilNextLevel - rowsCleared;

		if (rowsUntilNextLevel <= 0) {
			level = level + 1;
			rowsUntilNextLevel = 10;
			audioPlayer.LevelUp ();
		}
	}

	private void RefreshScoreAndLevel() {
		display.text = string.Format ("Score {0}\nLevel {1}", score, level);
	}

	public int GetScore() {
		return score;
	}

	public int GetLevel() {
		return level;
	}
}