using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public Text heading;
	public Text modeInstructions;
	public Text highScoresText;
	public InputField initialsInputField;
	public AudioPlayer audioPlayer;

	public Text scoreboardText;
	public List<TetrominoPrototype> tetrominos;
	public int[] gravity;

	public int[] scoreMultipliers;

	public int width;
	public int height;
	public Point spawnPoint;
	public Point previewPoint;

	private Mode currentMode;

	void Start () {
		ModeText modeText = new ModeText (heading, modeInstructions);
		Leaderboard leaderboard = new Leaderboard (highScoresText, initialsInputField);
		BlockField field = new BlockField(width,height);
		BlockMover mover = new BlockMover (field, spawnPoint, previewPoint, tetrominos);
		ScoreboardFactory scoreboardFactory = new ScoreboardFactory (scoreMultipliers, audioPlayer, scoreboardText);

		ReadyToPlay readyToPlay = new ReadyToPlay (modeText, leaderboard);
		LevelSelecter levelSelecter = new LevelSelecter (modeText);
		GameRunner gameRunner = new GameRunner (mover, scoreboardFactory, audioPlayer);
		GamePauser gamePauser = new GamePauser (modeText, audioPlayer);
		GameEnder gameEnder = new GameEnder (modeText, field);
		GameRestarter gameRestarter = new GameRestarter (modeText, leaderboard);

		gameRunner.gravity = gravity;

		readyToPlay.LevelSelecter = levelSelecter;
		levelSelecter.GameRunner = gameRunner;
		gameRunner.GamePauser = gamePauser;
		gameRunner.GameEnder = gameEnder;
		gameEnder.GameRestarter = gameRestarter;
		gameRestarter.StartScreen = readyToPlay;

		readyToPlay.Show ();
		currentMode = readyToPlay;
	}

	// Update is called once per frame
	void Update () {
		currentMode = currentMode.Update ();
	}	
}
