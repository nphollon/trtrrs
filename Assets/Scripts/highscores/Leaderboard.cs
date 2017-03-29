using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Leaderboard : MonoBehaviour {
	public String highScoresFile;
	public int lowestRank;
	public Text highScoresList;
	public InputField initialsInputField;

	private HighScoreRepo scores;
	private int score;

	public void Start () {
		scores = HighScoreRepo.Load (highScoresFile);
		HideInput ();
	}

	public void Show() {
		highScoresList.text = FormatHighScores (lowestRank);
	}

	private String FormatHighScores(int count) {
		StringBuilder builder = new StringBuilder ();
		List<HighScoreRecord> highScores = scores.GetHighScores (count);

		foreach (HighScoreRecord score in highScores) {
			builder.AppendLine (FormatScoreRecord(score));
		}

		return builder.ToString ();
	}

	private String FormatScoreRecord(HighScoreRecord score) {
		return String.Format ("{0} {1}", score.score, score.name);
	}

	public void Hide() {
		highScoresList.text = "";
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
		String initials = FormatInitialsForSubmission (initialsInputField.text);
		scores.AddScore (initials, score);
		HideInput ();
	}

	private string FormatInitialsForSubmission(string raw) {
		int length = initialsInputField.characterLimit;
		String paddedInitials = raw.PadLeft (length);
		return paddedInitials.ToUpper ();
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

