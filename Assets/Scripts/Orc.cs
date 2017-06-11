using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour {
	public bool isCarrotAttack;
	public Vector3 startPoint;
	public Vector3 destinationPoint;
	public float speed = 1;
	Transform heroParent = null;
	Rigidbody2D myBody = null;
	SpriteRenderer sr = null;
	public bool isLive=true;
	public BoxCollider2D head;
	public BoxCollider2D body;
	protected Mode mode;
	void Start () {
		startPoint= this.transform.position;
		mode=Mode.GoToB;
		//Зберегти стандартний батьківський GameObject
		this.heroParent = this.transform.parent;
		myBody = this.GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer>();

	}

	void FixedUpdate () {
		if (isLive) {
			

			float value = this.getDirection ();
			Animator animator = GetComponent<Animator> ();
			if (Mathf.Abs (value) > 0) {
				Vector2 vel = myBody.velocity;
				vel.x = value * speed;
				myBody.velocity = vel;
			}
			if (value < 0) {
				sr.flipX = false;
			} else if (value > 0) {
				sr.flipX = true;
			}

			if (Mathf.Abs (value) > 0) {
				animator.SetBool ("walk", true);
			} else {
				animator.SetBool ("walk", false);
			}


		}
	}

	float getDirection(){
		Vector3 rabit_pos = HeroRabit.lastRabit.transform.position;
		Vector3 my_pos = this.transform.position;
		if (isCarrotAttack) {
			if (attack ())
				return 0;
		}
		//Перевірка чи кролик зайшов в зону патрулювання
		if (rabit_pos.x >= Mathf.Min (startPoint.x, destinationPoint.x)&& rabit_pos.x <= Mathf.Max (startPoint.x, destinationPoint.x)){
			mode = Mode.Attack;
		}else {
			if(my_pos.x > destinationPoint.x) 
				this.mode = Mode.GoToA;
			if(my_pos.x < startPoint.x) 
				this.mode = Mode.GoToB;
		}

		if(this.mode == Mode.GoToB) {
			if(my_pos.x >= destinationPoint.x) {
				this.mode = Mode.GoToA;
			} 
		}
		else if(this.mode == Mode.GoToA) {
			if(my_pos.x <=startPoint.x) {
				this.mode = Mode.GoToB;
			} 
		}
		if (this.mode == Mode.AttackCarrot) {
			return 0;
		}
		if(this.mode == Mode.GoToB) {
			if(my_pos.x <= destinationPoint.x) {
				return 1;
			} else {
				return -1;
			}
		}else if(this.mode == Mode.GoToA) {
			if(my_pos.x >=startPoint.x) {
				return -1;
			} else {
				return 1;
			}
		}
		if(this.mode == Mode.Attack) {
			if(my_pos.x < rabit_pos.x) {
				return 1;
			} else {
				return -1;
			}
		}
		return 0;
	}
		
	public virtual bool attack () {
		return false;
	}

	public IEnumerator dieAnimation(){
		Animator animator = GetComponent<Animator> ();
		foreach(BoxCollider2D colliders in this.GetComponents<BoxCollider2D> () )
			colliders.enabled = false;
		Destroy (this.myBody);
		animator.SetBool ("die",true);
		yield return new WaitForSeconds(3.0f);
		Destroy (this.gameObject);
	 }

	public void dead(){
		StartCoroutine (dieAnimation ());
		isLive=false;
		mode = Mode.Dead;
	}

	public enum Mode {
		GoToA,
		GoToB,
		Attack,
		Dead,
		AttackCarrot
	}
	
}
