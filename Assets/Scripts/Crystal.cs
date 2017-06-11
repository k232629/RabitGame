using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {
	public CrystalColor color;
	protected override void OnRabitHit (HeroRabit rabit){
		LevelController.current.addCrystals (color);
		this.CollectedHide ();
	}

}