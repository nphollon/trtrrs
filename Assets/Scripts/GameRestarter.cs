using System;
using UnityEngine;
using UnityEngine.UI;

public class GameRestarter : Mode {
	private ModeText modeText;
	private Leaderboard leaderboard;
	private ReadyToPlay startScreen;

	public GameRestarter(ModeText modeText, Leaderboard leaderboard) {
		this.modeText = modeText;
		this.leaderboard = leaderboard;
	}

	public ReadyToPlay StartScreen {
		set { startScreen = value; }
	}

	public void CheckForHighScore(int score) {
		leaderboard.GetInitialsFor (score);

		if (leaderboard.WaitingForSubmission ()) {
			modeText.Print("High Score!", "Press Enter to Submit");
		}
	}

	public Mode Update () {
		leaderboard.CheckForSubmission ();

		if (leaderboard.WaitingForSubmission ()) {
			return this;
		} else {
			return ToStartScreen ();
		}
	}

	private Mode ToStartScreen() {
		startScreen.Show ();
		return startScreen;
	}
}
