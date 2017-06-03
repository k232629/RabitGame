using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {
	
	protected override void OnRabitHit (HeroRabit rabit){
		StartCoroutine(rabit.bombAction ());
		this.CollectedHide ();


		
	}
}