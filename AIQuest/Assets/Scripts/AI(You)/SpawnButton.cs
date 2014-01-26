using UnityEngine;
using System.Collections;
using System;

public class SpawnButton : MonoBehaviour {

	public float chargeLevel;
	public bool timerOn;
	public Transform monster;
	public Transform particleEffect;
    public string keyboardInput;


    KeyCode keyboardButton;

    private Transform buttonPosition;
	void Start () {

        buttonPosition = GetComponent<Transform>();
        keyboardButton = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardInput.ToUpper());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(keyboardButton) ){
			timerOn = true;
		}
		if (timerOn) {
			chargeLevel++;
		}
		if (Input.GetKeyUp (keyboardButton)) {
			Spawn (chargeLevel);
			chargeLevel = 0;
			timerOn = false;
		}
	}
	
	void OnMouseDown(){
		timerOn = true;
	}

	void OnMouseUp(){
		Spawn (chargeLevel);
		chargeLevel = 0;
		timerOn = false;
		}
	
	void Spawn(float charge){
		Transform temp = (Transform)Instantiate(monster, buttonPosition.position, Quaternion.identity);
		Monster currMonster = temp.GetComponent<Monster>();
		currMonster.setCharge(charge);
		Instantiate(monster, buttonPosition.position, Quaternion.identity);
	}
}
