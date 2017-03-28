using UnityEngine;

public class AudioPlayer : MonoBehaviour {
	public AudioSource music;
	public AudioSource levelUpSound;
	public AudioSource[] landingSounds;

	public void StartMusic() {
		music.Play ();
	}

	public void PauseMusic() {
		music.Pause ();
	}

	public void UnPauseMusic() {
		music.UnPause ();
	}

	public void StopMusic() {
		music.Stop ();
	}

	public void LevelUp() {
		levelUpSound.Play ();
	}

	public void LandBlock(int rowsCleared) {
		landingSounds [rowsCleared].Play ();
	}
}
