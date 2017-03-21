using System;
using UnityEngine;
using UnityEngine.UI;

public class GameEnder : Mode {
	private ModeText modeText;
	private BlockField field;
	private int score;
	private GameRestarter gameRestarter;

	public GameEnder(ModeText modeText, BlockField field) {
		this.modeText = modeText;
		this.field = field;
	}

	public void End(int score) {
		this.score = score;
		modeText.Print("Game Over", "Press Enter to Continue");
	}

	public GameRestarter GameRestarter {
		set { gameRestarter = value; }
	}			

	public Mode Update() {
		if (Input.GetKeyDown (KeyCode.Return)) {
			return Restart ();
		} else {
			return this;
		}
	}

	private Mode Restart() {
		field.Destroy ();
		gameRestarter.CheckForHighScore (score);
		return gameRestarter;
	}
}
