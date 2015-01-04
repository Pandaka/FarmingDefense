﻿using UnityEngine;
using System.Collections;

public class spawn_script : MonoBehaviour {

	public float interval = 5.0f;
	
	float timeLeft = 0.0f;
	
	public GameObject mob = null;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		
		if (timeLeft <= 0.0f) {

			GameObject g = (GameObject)Instantiate(mob, transform.position, Quaternion.identity);
			timeLeft = interval;
		}
	}
}
