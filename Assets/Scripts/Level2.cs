using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour {
	public SpriteRenderer fruit;
	public SpriteRenderer crystal;
	public SpriteRenderer complated;
	public SpriteRenderer locked;

	void Start(){
		if (PlayerPrefs.GetInt ("isFruitsCollectedL2", 0) == 0)
			Destroy (fruit);
		if (PlayerPrefs.GetInt ("isCrystalsCollectedL2", 0) == 0)
			Destroy (crystal);
		if (PlayerPrefs.GetInt ("Level2", 0) == 0)
			Destroy (complated);
		if (PlayerPrefs.GetInt ("Level1", 0) == 1)
			Destroy (locked);
		
	}


	void OnTriggerEnter2D(Collider2D collider) {
		SceneManager.LoadScene ("Level2");
	}
}