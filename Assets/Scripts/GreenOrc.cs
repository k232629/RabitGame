using UnityEngine;
using System.Collections;

public class GreenOrc : Orc {

	public override bool attack (){
		if(SoundManager.Instance.isSoundOn())
			attackSource.Play ();
		Animator animator = GetComponent<Animator> ();
		animator.SetTrigger ("attack");
		return true;
	}
}


