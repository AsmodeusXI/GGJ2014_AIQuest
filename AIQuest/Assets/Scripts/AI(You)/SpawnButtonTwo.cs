using UnityEngine;
using System.Collections;

public class SpawnButtonTwo : MonoBehaviour {
	
	public Transform monsterTwo;
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
		Instantiate(monsterTwo, new Vector3(-1.75f, -2.5f, 0f), Quaternion.identity);
	}
}