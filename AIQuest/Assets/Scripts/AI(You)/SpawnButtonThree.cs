using UnityEngine;
using System.Collections;

public class SpawnButtonThree : MonoBehaviour {
	
	public Transform monsterThree;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("e")) {
			Spawn();
		}
	}
	
	void OnMouseDown(){
		Spawn();
	}
	
	void Spawn(){
		// ... instantiate the rocket facing right and set it's velocity to the right. 
		Instantiate (monsterThree, new Vector3 (1.75f, -2.5f, 0f), Quaternion.identity);
	}
}