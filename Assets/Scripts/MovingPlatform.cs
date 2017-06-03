using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Vector3 MoveBy;
	Vector3 pointA;
	Vector3 pointB;
	bool going_to_a;// спочатку рухається в точку A з точки B
	public float time_to_wait = 0.5f;
	public float delay =0.5f;
	public Vector3 speed;
	

	// Use this for initialization
	void Start () {
		this.pointA = this.transform.position;
		this.pointB = this.pointA + MoveBy;	
		this.time_to_wait = Time.deltaTime;
		this.going_to_a=false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}  

	bool isArrived(Vector3 pos, Vector3 target) {
		pos.z = 0;
		target.z = 0;
	return Vector3.Distance(pos, target) < 0.02f;
	}

	void FixedUpdate(){
		time_to_wait -= Time.deltaTime;
		if(time_to_wait <= 0) {
			time_to_wait = delay;

			Vector3 my_pos = this.transform.position;
			Vector3 target;

			if(going_to_a) {
				target = this.pointA;
			} else {
				target = this.pointB;
			}
			Vector3 destination = target - my_pos;
			destination.z = 0;

			if (going_to_a) {
				this.transform.Translate(speed * Time.deltaTime);
			} else {
				this.transform.Translate(-speed * Time.deltaTime);
			}

			if(isArrived(my_pos,target)){
				this.transform.Translate(speed * Time.deltaTime);
				going_to_a=!going_to_a;
			}

		}
	}
}