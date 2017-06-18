using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {
	public AudioClip sound = null;
	AudioSource soundSource = null;
	public MyButton retryButton;
	public MyButton nextButton;
	public MyButton blackBacground;
	public MyButton closeButton;
	public UILabel collectedCoins;
	public UILabel collectedFruit;
	public Sprite crysalNot;
	public UI2DSprite crystalBlue;
	public UI2DSprite crystalGreen;
	public UI2DSprite crystalRed;
	public UILabel allFruits;
	int level;

	// Use this for initialization
	void Awake(){
		level = int.Parse(SceneManager.GetActiveScene ().name.Substring (SceneManager.GetActiveScene ().name.Length-1));
	}

	void Start () {
		soundSource = gameObject.AddComponent<AudioSource> ();
		soundSource.clip = sound;
		soundSource.Play ();
		blackBacground.signalOnClick.AddListener (close);
		closeButton.signalOnClick.AddListener (close);
		retryButton.signalOnClick.AddListener (this.retry);
		nextButton.signalOnClick.AddListener (this.menu);
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

	public void setFruit(int f){
		collectedFruit.text = f.ToString();
		int allFruitsInt = int.Parse(allFruits.text.Substring (1));

		if (allFruitsInt == f)
			PlayerPrefs.SetInt ("isFruitsCollectedL" + level.ToString (), 1);
		PlayerPrefs.Save ();

	}

	public void setCoins(int c){
		collectedCoins.text = c.ToString();

	}

	public void setCrystal(Crystals crystals){
		if (!crystals.getCrystal (CrystalColor.Blue))
			crystalBlue.sprite2D = crysalNot;
		if (!crystals.getCrystal (CrystalColor.Green))
			crystalGreen.sprite2D = crysalNot;
		if (!crystals.getCrystal (CrystalColor.Red))
			crystalRed.sprite2D = crysalNot;
		if (crystals.getCrystal (CrystalColor.Blue) && crystals.getCrystal (CrystalColor.Green) && crystals.getCrystal (CrystalColor.Red)) {
			PlayerPrefs.SetInt ("isCrystalsCollectedL" + level, 1);
			PlayerPrefs.Save ();
		}
	}
}
