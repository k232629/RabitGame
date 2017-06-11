using UnityEngine;
using System.Collections;

public class GreenOrc : Orc {

	public override bool attack (){
		Animator animator = GetComponent<Animator> ();
		animator.SetTrigger ("attack");
		return true;
	}
}


