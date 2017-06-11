using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	public MyButton startButton;

	// Use this for initialization
	void Start () {
		startButton.signalOnClick.AddListener (this.start);
	}
	

	void start () {
		SceneManager.LoadScene ("Main");
	}
}
