using System;
using UnityEngine;
using UnityEngine.UI;

public interface Scoreboard {
	void Record (int rowsCleared);
	int GetScore ();
	int GetLevel ();
}

[Serializable]
public class Reward {
	public int points;
	public AudioSource sound;

	public AudioSource Sound {
		get { return sound; }
	}

	public int Points {
		get { return points; }
	}
}

public class ScoreboardFactory {
	private Reward[] scoreRewards;
	private AudioSource levelUpSound;
	private Text display;

	public ScoreboardFactory(AudioSource levelUpSound, Reward[] scoreRewards, Text display) {
		this.scoreRewards = scoreRewards;
		this.levelUpSound = levelUpSound;
		this.display = display;
	}

	public Scoreboard StartAtLevel(int startingLevel) {
		return new ScoreboardWithAudio (startingLevel, levelUpSound, scoreRewards, display);
	}
}

class ScoreboardWithAudio : Scoreboard {
	private int score;
	private int level;
	private int rowsUntilNextLevel;

	private Reward[] scoreRewards;
	private AudioSource levelUpSound;
	private Text display;

	public ScoreboardWithAudio(int startingLevel, AudioSource levelUpSound, Reward[] scoreRewards, Text display) {
		score = 0;
		level = startingLevel;
		rowsUntilNextLevel = startingLevel * 10 + 10;

		this.scoreRewards = scoreRewards;
		this.levelUpSound = levelUpSound;
		this.display = display;

		RefreshScoreAndLevel();
	}

	public void Record(int rowsCleared) {
		UpdateScore (rowsCleared);
		UpdateLevel (rowsCleared);
		RefreshScoreAndLevel ();
	}

	private void UpdateScore(int rowsCleared) {
		int rewardIndex = Math.Min (rowsCleared, scoreRewards.Length - 1);
		Reward reward = scoreRewards [rewardIndex];

		reward.Sound.Play ();
		score = score + reward.points * level;
	}

	private void UpdateLevel(int rowsCleared) {
		rowsUntilNextLevel = rowsUntilNextLevel - rowsCleared;

		if (rowsUntilNextLevel <= 0) {
			level = level + 1;
			rowsUntilNextLevel = 10;
			levelUpSound.Play ();
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