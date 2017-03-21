using System;

public class HighScoreRecord {
	public readonly String name;
	public readonly int score;

	public HighScoreRecord(String name, int score) {
		this.name = name;
		this.score = score;
	}

	public static int Compare(HighScoreRecord a, HighScoreRecord b) {
		if (a.score == b.score) {
			return a.name.CompareTo (b.name);
		} else {
			return a.score.CompareTo (b.score);
		}
	}
}
