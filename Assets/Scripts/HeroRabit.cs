﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {
	public bool startLevel;
	public static HeroRabit lastRabit = null;
	bool isGrounded = false;
	bool JumpActive = false;
	bool bigRabit = false;
	bool rabit = true;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;
	
	public float speed = 1;

	Transform heroParent = null;
	
	Rigidbody2D myBody = null;
	SpriteRenderer sr = null;
	public AudioClip walkSound = null;
	public AudioClip touchdownSound = null;
	public AudioClip dieSound = null;
	AudioSource walkSource = null;
	AudioSource touchdownSource = null;
	AudioSource dieSource = null;
	void Awake() {
		lastRabit = this;
	}

	void Start () {
		walkSource = gameObject.AddComponent<AudioSource> ();
		walkSource.clip = walkSound;
		touchdownSource = gameObject.AddComponent<AudioSource> ();
		touchdownSource.clip = touchdownSound;
		dieSource = gameObject.AddComponent<AudioSource> ();
		dieSource.clip = dieSound;
		//Зберегти стандартний батьківський GameObject
		this.heroParent = this.transform.parent;
		myBody = this.GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer>();
		LevelController.current.setStartPosition (transform.position);

	}


	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		//[-1, 1]
		if (rabit&&!startLevel) {
			Vector3 from = transform.position + Vector3.up * 0.3f;
			Vector3 to = transform.position + Vector3.down * 0.1f;
			int layer_id = 1 << LayerMask.NameToLayer ("Ground");
			//Перевіряємо чи проходить лінія через Collider з шаром Ground
			RaycastHit2D hit = Physics2D.Linecast (from, to, layer_id);
			if (hit) {
				isGrounded = true;
			} else {
				isGrounded = false;
			}

			//Згадуємо ground check
			if (hit) {
				if (hit.transform != null && hit.transform.GetComponent<MovingPlatform> () != null) {
					//Приліпаємо до платформи
					SetNewParent (this.transform, hit.transform);
				}
			} else {
				//Ми в повітрі відліпаємо під платформи
				SetNewParent (this.transform, this.heroParent);
			}


			if (Input.GetButtonDown ("Jump") && isGrounded) {
				this.JumpActive = true;
			}
			if (this.JumpActive) {
				//Якщо кнопку ще тримають
				if (Input.GetButton ("Jump")) {
					this.JumpTime += Time.deltaTime;
					if (this.JumpTime < this.MaxJumpTime) {
						Vector2 vel = myBody.velocity;
						vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
						myBody.velocity = vel;
					}
				} else {
					this.JumpActive = false;
					this.JumpTime = 0;
				}
			}
			float value = Input.GetAxis ("Horizontal");
			Animator animator = GetComponent<Animator> ();

			if (Mathf.Abs (value) > 0) {
				Vector2 vel = myBody.velocity;
				vel.x = value * speed;
				myBody.velocity = vel;
			}

			if (value < 0) {
				sr.flipX = true;
			} else if (value > 0) {
				sr.flipX = false;
			}

			if (Mathf.Abs (value) > 0) {
				
				animator.SetBool ("run", true);
			} else {
				if(SoundManager.Instance.isSoundOn())
					walkSource.Play ();
				else
					walkSource.Pause ();
				animator.SetBool ("run", false);
			}
			if (this.isGrounded) {
				
				animator.SetBool ("jump", false);
			} else {
				if(SoundManager.Instance.isSoundOn())
				touchdownSource.Play ();
				animator.SetBool ("jump", true);
			}

		}
	}



	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			//Засікаємо позицію у Глобальних координатах
			Vector3 pos = obj.transform.position;
			//Встановлюємо нового батька
			obj.transform.parent = new_parent;
			//Після зміни батька координати кролика зміняться
			//Оскільки вони тепер відносно іншого об’єкта
			//повертаємо кролика в ті самі глобальні координати
			obj.transform.position = pos;
	}
}


	public void becomeBigger(){
		if(!this.bigRabit){
			this.transform.localScale = Vector3.one * 2;
			this.bigRabit=true;
		}
	}

	public IEnumerator dieAnimation(){
		Animator animator = GetComponent<Animator> ();
		if(this.bigRabit){
			this.transform.localScale = Vector3.one;
			this.bigRabit=false;
		}
		else{
			rabit = false;
			animator.SetBool ("die",true);
			if(SoundManager.Instance.isSoundOn())
				dieSource.Play ();
			yield return new WaitForSeconds(1.5f);
			LevelController.current.onRabitDeath(this);
			animator.SetBool ("die",false);
			rabit = true;
		}
	 }

	public void dead(){
		
		StartCoroutine (dieAnimation ());
	}

	public void OnTriggerEnter2D(Collider2D collider){
		if (rabit) {
			GreenOrc greenOrc = collider.gameObject.GetComponent<GreenOrc> ();
			if (greenOrc != null) {
				if (greenOrc.isLive) {
					if (collider == greenOrc.body) {
						this.dead ();
						greenOrc.attack ();
					} else if (collider == greenOrc.head) {
						greenOrc.dead ();
					}
				}
			}
		}
		if (rabit) {
			BrownOrc brownOrc = collider.gameObject.GetComponent<BrownOrc> ();
			if (brownOrc != null) {
				if (brownOrc.isLive) {
					if (collider == brownOrc.head) {
						brownOrc.dead ();
					}
				}
			}
		}
	}
		
}
