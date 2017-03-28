using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauser : Mode {
	private ModeText modeText;
	private GameRunner pausedRunner;
	private AudioPlayer audio;

	public GamePauser(ModeText modeText, AudioPlayer audio) {
		this.modeText = modeText;
		this.audio = audio;
	}

	public void Pause(GameRunner gameRunner) {
		modeText.Print("Game Paused", "Press Enter to Resume");
		audio.PauseMusic ();
		pausedRunner = gameRunner;
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
		audio.UnPauseMusic ();
		return pausedRunner;
	}
}