using UnityEngine;
using System.Collections;
using System;

public class SpawnButton : MonoBehaviour {
	
	public Transform monster;
    public string keyboardInput;


    KeyCode keyboardButton;

    private Transform buttonPostion;
	void Start () {

        buttonPostion = GetComponent<Transform>();
        keyboardButton = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardInput.ToUpper());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(keyboardButton) ){
			Spawn();
		}
	}
	
	void OnMouseDown(){
		Spawn();
	}
	
	void Spawn(){
		// ... instantiate the rocket facing right and set it's velocity to the right. 
		Instantiate(monster, buttonPostion.position, Quaternion.identity);
	}
}