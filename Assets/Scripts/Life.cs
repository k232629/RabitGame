using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Life : Collectable {



	protected override void OnRabitHit (HeroRabit rabit){
		LevelController.current.addLife ();
		this.CollectedHide ();
	}
}