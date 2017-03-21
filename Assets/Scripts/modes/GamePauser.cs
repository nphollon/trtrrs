using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauser : Mode {
	private ModeText modeText;
	private GameRunner pausedRunner;

	public GamePauser(ModeText modeText) {
		this.modeText = modeText;
	}

	public void Pause(GameRunner gameRunner) {
		pausedRunner = gameRunner;
		modeText.Print("Game Paused", "Press Enter to Resume");
	}

	public Mode Update() {
		if (Input.GetKeyDown (KeyCode.Return)) {
			return Resume ();
		} else {
			return this;
		}
	}

	private Mode Resume() {
		modeText.Clear ();
		return pausedRunner;
	}
}