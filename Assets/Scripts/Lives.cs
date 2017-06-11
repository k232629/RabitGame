using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour {
	public Sprite liveFull;
	public Sprite liveEmpty;
	public List <UI2DSprite> lives;

	public void setLives(int lives){
		for (int i = 0; i < 3; i++) {
			if (i < lives) {
				this.lives [i].sprite2D = this.liveFull;
			} else {
				this.lives [i].sprite2D = this.liveEmpty;
			}
		}
	}
}
