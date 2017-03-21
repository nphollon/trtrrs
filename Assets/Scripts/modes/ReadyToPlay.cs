using System;
using UnityEngine;
using UnityEngine.UI;

public class ReadyToPlay : Mode {
	private ModeText modeText;
	private Leaderboard leaderboard;
	private LevelSelecter levelSelection;

	public ReadyToPlay(ModeText modeText, Leaderboard leaderboard) {
		this.modeText = modeText;
		this.leaderboard = leaderboard;
	}

	public LevelSelecter LevelSelecter {
		set { levelSelection = value; }
	}

	public void Show () {
		leaderboard.Show ();
		modeText.Print("", "Press Enter to Start");
	}

	public Mode Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			return SelectLevel();
		} else {
			return this;
		}
	}

	private Mode SelectLevel () {
		leaderboard.Hide ();
		levelSelection.Show ();
		return levelSelection;
	}
}