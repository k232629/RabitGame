using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
	public MyButton blackBacground;
	public MyButton closeButton;
	public MyButton soundButton;
	public MyButton musicButton;
	public Sprite musicOn;
	public Sprite musicOff;
	public Sprite soundOn;
	public Sprite soundOff;
	bool music=true;
	bool sound =true;
	// Use this for initialization
	void Start () {
		blackBacground.signalOnClick.AddListener (close);
		closeButton.signalOnClick.AddListener (close);
		soundButton.signalOnClick.AddListener (this.changeSound);
		musicButton.signalOnClick.AddListener (this.changeMusic);
		if (!SoundManager.Instance.isSoundOn()) {
			soundButton.GetComponent<UIButton> ().normalSprite2D = soundOff;
			sound = false;
		}
		if (!SoundManager.Instance.isMusicOn()) {
			musicButton.GetComponent<UIButton> ().normalSprite2D = musicOff;
			music = false;
		}
	}


	void close () {
		Destroy (this.gameObject);
	}

	void changeMusic(){
		if (music) {
			musicButton.GetComponent<UIButton> ().normalSprite2D = musicOff;
			music = false;
			SoundManager.Instance.setMusicOn (false);
			LevelController.current.setMusicOff ();
		} else {
			musicButton.GetComponent<UIButton> ().normalSprite2D = musicOn;
			music = true;
			SoundManager.Instance.setMusicOn (true);
			LevelController.current.setMusicOn ();
		}
	}

	void changeSound(){
		if (sound) {
			soundButton.GetComponent<UIButton> ().normalSprite2D = soundOff;
			sound = false;
			SoundManager.Instance.setSoundOn (false);
		} else {
			sound = true;
			soundButton.GetComponent<UIButton> ().normalSprite2D = soundOn;
			SoundManager.Instance.setSoundOn (true);
		}
	}
}
