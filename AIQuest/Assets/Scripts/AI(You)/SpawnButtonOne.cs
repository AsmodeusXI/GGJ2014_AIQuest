﻿using UnityEngine;
using System.Collections;

public class SpawnButtonOne : MonoBehaviour {
	
	public Transform monsterOne;
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
		Instantiate(monsterOne, new Vector3(-5f, -2.5f, 0f), Quaternion.identity);
	}
}