using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Fruit : Collectable {

	void Start(){
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		if (PlayerPrefs.GetInt (SceneManager.GetActiveScene().name+this.name, 0) == 1) {
			Color color = sprite.color;
			color.a = 0.5f;
			sprite.color = color;
		}
	}

	protected override void OnRabitHit (HeroRabit rabit){
		LevelController.current.addFruits (1,this);
		this.CollectedHide ();
	}
}