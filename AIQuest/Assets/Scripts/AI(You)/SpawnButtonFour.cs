﻿using UnityEngine;
using System.Collections;

public class SpawnButtonFour : MonoBehaviour {
	
	public Transform monsterFour;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown(){
		Spawn();
	}
	
	void Spawn(){
		// ... instantiate the rocket facing right and set it's velocity to the right. 
		Instantiate(monsterFour, new Vector2(-10, 0), transform.rotation);
	}
}