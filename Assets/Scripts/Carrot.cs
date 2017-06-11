using System.Collections;
using UnityEngine;

public class Carrot : Collectable {
	float my_direction;
	public float speed;
	public float destroyTime;


	protected override void OnRabitHit (HeroRabit rabit){
		rabit.dead ();
		this.CollectedHide ();
	}

	public void launch (float direction){
		this.my_direction = direction;
		if (direction < 0) {
			this.GetComponent<SpriteRenderer> ().flipX = true;
		}
		StartCoroutine (destroy());
	}
		
	void Update(){
		Vector3 pos = this.transform.position;
		pos.x += Time.deltaTime * my_direction * speed;
		this.transform.position = pos;
	}

	IEnumerator destroy(){
		yield return new WaitForSeconds (destroyTime);
		Destroy (this.gameObject);
	}
}
