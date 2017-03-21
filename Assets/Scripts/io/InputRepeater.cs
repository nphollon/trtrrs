using System;

public delegate bool TetrominoMotion();

public class InputRepeater {
	private readonly double initialDelay;
	private readonly double repeatDelay;
	private TetrominoMotion moveTetromino;
	private double timeTilMotion;
	private bool keyIsDown;
	private bool firstReset;

	public InputRepeater(double initialDelay, double repeatDelay, TetrominoMotion moveTetromino) {
		this.initialDelay = initialDelay;
		this.repeatDelay = repeatDelay;
		this.moveTetromino = moveTetromino;
		ReleaseKey ();
	}

	public void SetInput(bool inputIsOn) {
		if (KeyStateIsChanging (inputIsOn)) {
			ToggleKey ();
		}
	}

	private bool KeyStateIsChanging (bool newState) {
		return newState != keyIsDown;
	}

	private void ToggleKey() {
		if (keyIsDown) {
			ReleaseKey ();
		} else {
			PressKey ();
		}
	}

	private void PressKey() {
		keyIsDown = true;
		firstReset = true;
		timeTilMotion = 0;
	}

	private void ReleaseKey() {
		keyIsDown = false;
	}

	public void NextFrame(double elapsedTime) {
		if (KeyIsDown()) {
			DecreaseTimer (elapsedTime);
			if (TimerFinished()) {
				moveTetromino ();
				ResetTimer();
			}
		}
	}

	private bool KeyIsDown() {
		return keyIsDown;
	}

	private void DecreaseTimer(double elapsedTime) {
		timeTilMotion = timeTilMotion - elapsedTime;
	}

	private bool TimerFinished() {
		return timeTilMotion <= 0;
	}

	private void ResetTimer() {
		if (firstReset) {
			timeTilMotion = initialDelay;
			firstReset = false;
		} else {
			timeTilMotion = repeatDelay;
		}
	}
}