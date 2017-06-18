using UnityEngine;
using System.Collections;

public class BrownOrc : Orc {
	public float distantToRabit;
	public GameObject carrot;
	public float time;
	float lastCarrot=0;
	public override bool attack (){
		Vector3 rabit_pos = HeroRabit.lastRabit.transform.position;
		Vector3 my_pos = this.transform.position;
		if (Mathf.Abs (rabit_pos.x - my_pos.x) < distantToRabit) {
			if (isLive) {
				launchCarrot ();
				return true;
			}
		} 
		return false;
		
	}

	void launchCarrot() {
		Animator animator = GetComponent<Animator> ();
		Vector3 my_pos = this.transform.position;
		Vector3 rabit_pos = HeroRabit.lastRabit.transform.position;
			if (Time.time - this.lastCarrot > time) {
				if(SoundManager.Instance.isSoundOn())
					attackSource.Play ();
				animator.SetTrigger ("attack");
				this.lastCarrot = Time.time;
				GameObject gameObject = GameObject.Instantiate (this.carrot);
				gameObject.transform.position = my_pos + Vector3.up * 0.5f;
				Carrot carrot = gameObject.GetComponent<Carrot> ();
				if (rabit_pos.x < my_pos.x) 
					carrot.launch (-1);
				 else 
					carrot.launch (1);
			}
	}
}

