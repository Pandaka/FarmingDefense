﻿using UnityEngine;
using System.Collections;

public class StatMob : MonoBehaviour {

	public int attaque = 1;
	public int defense = 1;
	public int vitesse = 1;
	public int cout = 1;
	public int moneydrop= 50;

	public void onDeath() {
		PlayerScript.billet += moneydrop;
		this.gameObject.SetActive(false);
	}

	public void hit(int degat) {
		defense-=degat;
		if(defense<=0)
			onDeath();
	}

}