using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelecter : Mode {
	private ModeText modeText;
	private int startingLevel;
	private GameRunner gameRunner;

	public LevelSelecter(ModeText modeText) {
		this.modeText = modeText;
		startingLevel = 1;
	}

	public GameRunner GameRunner {
		set { gameRunner = value; }
	}

	public void Show () {
		DisplayStartingLevel ();
	}

	public Mode Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			return StartGame ();
		} else {
			CheckForLevelChange ();
			return this;
		}
	}

	private Mode StartGame () {
		modeText.Clear ();
		gameRunner.BeginAtLevel (startingLevel);
		return gameRunner;
	}

	private void CheckForLevelChange() {
		if (Input.GetKeyDown (KeyCode.A)) {
			SetStartingLevel (startingLevel - 1);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			SetStartingLevel (startingLevel + 1);
		}
	}

	private void SetStartingLevel(int newLevel) {
		startingLevel = Math.Max(1, newLevel);
		DisplayStartingLevel ();
	}

	private void DisplayStartingLevel() {
		string heading = String.Format ("Level: {0}", startingLevel);
		string subheading = "Set Level with A and D\nPress Enter to Start";
		modeText.Print (heading, subheading);
	}
}
