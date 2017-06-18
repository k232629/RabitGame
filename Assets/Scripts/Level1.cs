using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour {
	public SpriteRenderer fruit;
	public SpriteRenderer crystal;
	public SpriteRenderer complated;

	void Start(){
		if (PlayerPrefs.GetInt ("isFruitsCollectedL1", 0) == 0)
			Destroy (fruit);
		if (PlayerPrefs.GetInt ("isCrystalsCollectedL1", 0) == 0)
			Destroy (crystal);
		if (PlayerPrefs.GetInt ("Level1", 0) == 0)
			Destroy (complated);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		SceneManager.LoadScene ("Level1");
	}
}