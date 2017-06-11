using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour {
	

	public static LevelController current;
		public Lives lives;
		public Crystals crystals;
		int numberOfLives=3;
		public UILabel labelCoins;
		public UILabel labelFruits;
		public UILabel allCoins;
		public int coins=0;
		public int fruits=0;
		

		void Awake() {
			current = this;

		}

		void Start(){
			if(lives!=null)
			this.lives.setLives (this.numberOfLives);
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
		else
			SceneManager.LoadScene ("Main");
		

		}

		public void addCoins(int n){
			coins+=n;
			string label="000"+coins.ToString ();
			label.Substring (label.Length-4,4);
			labelCoins.text = label;
		}

		public void addFruits(int n){
			fruits+=n;
		labelFruits.text = fruits.ToString ();

		}

	public void addCrystals(CrystalColor color){
			crystals.addCrystal (color);
		}



}