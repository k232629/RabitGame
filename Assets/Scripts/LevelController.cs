using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
	

	public static LevelController current;

	public int coins=0;
	public int fruits=0;
	public int crystals=0;


		void Awake() {
			current = this;
		}

		Vector3 startingPosition;
		public void setStartPosition(Vector3 pos) {
			this.startingPosition = pos;
		}


		public void onRabitDeath(HeroRabit rabit) {
			//При смерті кролика повертаємо на початкову позицію
			rabit.transform.position = this.startingPosition;
		}

		public void addCoins(int n){
			coins+=n;

		}

		public void addFruits(int n){
			fruits+=n;

		}

		public void addCrystals(int n){
			crystals+=n;

		}
	
}