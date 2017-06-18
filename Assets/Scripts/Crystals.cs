using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : MonoBehaviour {
	Dictionary<CrystalColor,bool> obtainCrystal= new Dictionary<CrystalColor, bool>();
	public Sprite crystalNotFound;
	public List<Sprite> crystalColors;
	public List <UI2DSprite> crystalPlace;

	public Dictionary<CrystalColor,bool> getObtainedCrystal(){
		return obtainCrystal;
	}

	// Use this for initialization
	void Start () {
		this.reloadCrystals ();
	}

	void reloadCrystals(){
		updateCrystalColor (CrystalColor.Green);
		updateCrystalColor (CrystalColor.Red);
		updateCrystalColor (CrystalColor.Blue);
	}

	public void addCrystal(CrystalColor color){
		obtainCrystal [color] = true;
		this.reloadCrystals ();
	}

	void updateCrystalColor(CrystalColor color){
		int crystal_id = (int)color;
		if (obtainCrystal.ContainsKey (color)) {
			crystalPlace [crystal_id].sprite2D =crystalColors [crystal_id];
		} else {
			crystalPlace [crystal_id].sprite2D = crystalNotFound;
		}
	}

	public bool getCrystal(CrystalColor color){
		return obtainCrystal.ContainsKey (color);
	}


}
