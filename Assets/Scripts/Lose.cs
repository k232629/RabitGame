using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour {
	public AudioClip sound = null;
	AudioSource soundSource = null;
	public MyButton retryButton;
	public MyButton menuButton;
	public MyButton blackBacground;
	public MyButton closeButton;

	// Use this for initialization
	void Start () {
		soundSource = gameObject.AddComponent<AudioSource> ();
		soundSource.clip = sound;
		soundSource.Play ();
		blackBacground.signalOnClick.AddListener (close);
		closeButton.signalOnClick.AddListener (close);
		retryButton.signalOnClick.AddListener (this.retry);
		menuButton.signalOnClick.AddListener (this.menu);
	}

	void retry(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		Time.timeScale = 1;
	}

	void menu(){
		SceneManager.LoadScene ("Main");
		Time.timeScale = 1;
	}

	void close () {
		Destroy (this.gameObject);
		menu ();
	}
}
