using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRunner : Mode {
	public int[] gravity;

	private InputRepeater leftShifter;
	private InputRepeater rightShifter;
	private InputRepeater spinShifter;

	private double timeTilDrop;

	private ScoreboardFactory scoreboardFactory;
	private Scoreboard scoreboard;
	private AudioPlayer audio;
	private BlockMover mover;

	private GamePauser pauser;
	private GameEnder gameOver;

	public GameRunner(BlockMover mover, ScoreboardFactory scoreboardFactory, AudioPlayer audio) {
		this.scoreboardFactory = scoreboardFactory;
		this.mover = mover;
		this.audio = audio;

		leftShifter = new InputRepeater (0.25, 0.02, mover.MoveLeft);
		rightShifter = new InputRepeater (0.25, 0.02, mover.MoveRight);
		spinShifter = new InputRepeater (0.5, 0.1, mover.RotateAntiClockwise);
	}

	public GamePauser GamePauser {
		set { pauser = value; }
	}

	public GameEnder GameEnder {
		set { gameOver = value; }
	}		

	public void BeginAtLevel(int startingLevel) {
		scoreboard = scoreboardFactory.StartAtLevel (startingLevel);
		audio.StartMusic ();
		mover.Reset ();
	}

	public Mode Update() {
		if (Input.GetAxis ("Pause") > 0) {
			pauser.Pause (this);
			return pauser;
		}

		leftShifter.SetInput (Input.GetAxis ("Horizontal") < 0);
		rightShifter.SetInput (Input.GetAxis ("Horizontal") > 0);
		spinShifter.SetInput (Input.GetAxis ("Vertical") > 0);

		leftShifter.NextFrame (Time.deltaTime);
		rightShifter.NextFrame (Time.deltaTime);
		spinShifter.NextFrame (Time.deltaTime);

		bool softDropping = Input.GetAxis ("Vertical") < 0;
		bool droppedOneLevel = UpdateDropClock (Time.deltaTime, softDropping);

		if (droppedOneLevel) {
			bool blockWasLowered = mover.MoveDown ();

			if (!blockWasLowered) {
				scoreboard.Record (mover.CountAndRemoveFullRows ());
				bool couldSpawn = mover.SpawnTetromino ();

				if (!couldSpawn) {
					audio.StopMusic ();
					gameOver.End (scoreboard.GetScore());
					return gameOver;
				}
			}
		}

		return this;
	}

	private bool UpdateDropClock(double deltaTime, bool softDropping) {
		double clockCeiling = DropSpeed (softDropping);

		timeTilDrop = Math.Min(clockCeiling, timeTilDrop - deltaTime);

		bool clockExpired = timeTilDrop <= 0;

		if (clockExpired) {
			timeTilDrop = clockCeiling;
		}
		return clockExpired;
	}

	private double DropSpeed(bool softDropping) {
		int framesPerDrop;
		int level = scoreboard.GetLevel ();

		if (softDropping || level > gravity.Length) {
			framesPerDrop = gravity [gravity.Length - 1];
		} else {
			framesPerDrop = gravity [level - 1];
		}

		return framesPerDrop / 60.0;
	}
}