using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour {
	bool isLevel1Completed;
	bool isLevel2Completed;
	public static LevelController current;
	public MyButton pause;
	public GameObject settingsPrefab;
	public GameObject losePrefab;
	public GameObject winPrefab;
	public Lives lives;
	public Crystals crystals;
	int numberOfLives=3;
	public UILabel labelCoins;
	public UILabel labelFruits;
	public UILabel mainSceneCoins;
	public int coins=0;
	public int fruits=0;
	public AudioClip music = null;
	AudioSource musicSource = null;
	public int allCollectedCoins;
	List<string> collectedFruitToSave;
		void Awake() {
			current = this;

		}

		void Start(){
		collectedFruitToSave = new List<string> ();
		allCollectedCoins = PlayerPrefs.GetInt ("allCollectedCoins",0);
		isLevel1Completed = PlayerPrefs.GetInt ("Level1", 0) == 1;
		isLevel2Completed = PlayerPrefs.GetInt ("Level2", 0) == 1;
		if (mainSceneCoins != null) {
			string label = "000" + allCollectedCoins.ToString ();
			label.Substring (label.Length - 4, 4);
			mainSceneCoins.text = label;
		}
			musicSource = gameObject.AddComponent<AudioSource>();
			musicSource.clip = music;
			musicSource.loop = true;
		if(SoundManager.Instance.isMusicOn())
			musicSource.Play ();

			if(lives!=null)
			this.lives.setLives (this.numberOfLives);
		if(pause!=null)
			pause.signalOnClick.AddListener (this.showSettings);
		}

		Vector3 startingPosition;
		public void setStartPosition(Vector3 pos) {
			this.startingPosition = pos;
		}


		public void onRabitDeath(HeroRabit rabit) {
			this.numberOfLives -= 1;
			if (lives != null)
				this.lives.setLives (this.numberOfLives);
		if (numberOfLives > 0)
			rabit.transform.position = this.startingPosition;
		else {
			//Знайти батьківський елемент
			GameObject parent = UICamera.first.transform.parent.gameObject;
			//Створити Prefab
			GameObject obj = NGUITools.AddChild (parent, losePrefab);
			Vector3 pos = new Vector3 (-7f,-3,0);
			Quaternion quat = new Quaternion (0,0,0,0);
			obj.transform.SetPositionAndRotation (pos,quat);
			Time.timeScale = 0;
		}
		

		}

		public void addCoins(int n){
			coins+=n;
			string label="000"+coins.ToString ();
			label.Substring (label.Length-4,4);
			labelCoins.text = label;
		}

	public void addFruits(int n,Fruit fruit){
		collectedFruitToSave.Add (SceneManager.GetActiveScene ().name + fruit.name);
			fruits+=n;
		labelFruits.text = fruits.ToString ();

		}

	public void addCrystals(CrystalColor color){
			crystals.addCrystal (color);
		}

	void showSettings() {
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, settingsPrefab);
		Vector3 pos = new Vector3 (-6.9f,-3,0);
		Quaternion quat = new Quaternion (0,0,0,0);
		obj.transform.SetPositionAndRotation (pos,quat);
		//Отримати доступ до компоненту (щоб передати параметри)
		Settings popup = obj.GetComponent<Settings>();
	}

	public void createWinPanel(string level){
		PlayerPrefs.SetInt (level,1);
		PlayerPrefs.SetInt ("allCollectedCoins", allCollectedCoins + coins);
		for (int i = 0; i < collectedFruitToSave.Count; i++) {
			PlayerPrefs.SetInt (collectedFruitToSave [i], 1);
		}
		PlayerPrefs.Save ();

		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, winPrefab);
		Time.timeScale = 0;
		Win win = obj.GetComponent<Win>();
		win.setCoins (coins);
		win.setFruit (fruits);
		win.setCrystal (crystals);
	
	}

	public void setMusicOff(){
		musicSource.Pause ();
	}

	public void setMusicOn(){
		musicSource.Play();
	}
}