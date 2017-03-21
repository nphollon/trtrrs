using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Leaderboard {
	private const String highScoresFile = "scores.txt";
	private const int lowestRank = 10;

	private Text display;
	private InputField initialsInputField;
	private HighScoreRepo scores;
	int score;

	public Leaderboard(Text display, InputField initialsInputField) {
		this.display = display;
		this.initialsInputField = initialsInputField;
		scores = HighScoreRepo.Load (highScoresFile);

		HideInput ();
	}

	public void Show() {
		display.text = scores.FormatHighScores (lowestRank);
	}

	public void Hide() {
		display.text = "";
	}

	public void GetInitialsFor(int score) {
		if (score > scores.GetScore(lowestRank)) {
			this.score = score;
			ShowInput ();
		}
	}
		
	public void CheckForSubmission() {
		if (WaitingForSubmission() && Input.GetKeyDown (KeyCode.Return)) {
			SubmitHighScore ();
		}
	}

	private void SubmitHighScore() {
		String initials = initialsInputField.text.ToUpper ();
		scores.AddScore (initials, score);
		HideInput ();
	}

	private void HideInput() {
		if (WaitingForSubmission()) {
			initialsInputField.enabled = false;
			EventSystem.current.SetSelectedGameObject (null);
			initialsInputField.text = "";
			initialsInputField.transform.Translate (new Vector3 (0, 1000));
		}
	}

	private void ShowInput() {
		if (!WaitingForSubmission()) {
			initialsInputField.enabled = true;
			initialsInputField.Select ();
			initialsInputField.transform.Translate (new Vector3 (0, -1000));
		}
	}

	public bool WaitingForSubmission() {
		return initialsInputField.enabled;
	}
}

