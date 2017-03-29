using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public AudioPlayer audioPlayer;
	public ModeText modeText;
	public ScoreboardFactory scoreboardFactory;
	public Leaderboard leaderboard;
	public BlockField blockField;
	public BlockMover blockMover;

	public int[] gravity;

	private Mode currentMode;

	void Start () {
		ReadyToPlay readyToPlay = new ReadyToPlay (modeText, leaderboard);
		LevelSelecter levelSelecter = new LevelSelecter (modeText);
		GameRunner gameRunner = new GameRunner (blockMover, scoreboardFactory, audioPlayer);
		GamePauser gamePauser = new GamePauser (modeText, audioPlayer);
		GameEnder gameEnder = new GameEnder (modeText, blockField);
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
