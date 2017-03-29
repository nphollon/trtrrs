using UnityEngine;

public class AudioPlayer : MonoBehaviour {
	public AudioSource music;
	public AudioSource levelUpSound;
	public AudioSource[] landingSounds;

	public float fadeIn;
	public float fadeOut;

	private float targetVolume;

	public void Start() {
		StopMusic ();
	}

	public void StartMusic() {
		targetVolume = 1;
	}

	public void PauseMusic() {
		music.Pause ();
	}

	public void UnPauseMusic() {
		music.UnPause ();
	}

	public void StopMusic() {
		targetVolume = 0;
	}

	public void LevelUp() {
		levelUpSound.Play ();
	}

	public void LandBlock(int rowsCleared) {
		landingSounds [rowsCleared].Play ();
	}

	public void Update() {
		if (music.volume > targetVolume) {
			music.volume -= Time.deltaTime / fadeOut;
		} else if (music.volume < targetVolume) {
			music.volume += Time.deltaTime / fadeIn;
		}
	}
}
