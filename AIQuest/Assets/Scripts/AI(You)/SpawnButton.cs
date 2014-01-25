using UnityEngine;
using System.Collections;
using System;

public class SpawnButton : MonoBehaviour {

	public float chargeLevel;
	public bool timerOn;
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
		Spawn(0f);
	}
	
	void Spawn(float charge){
		GameObject temp = (GameObject)Instantiate(monster, buttonPostion.position, Quaternion.identity);
		Monster currMonster = temp.GetComponent<Monster>();
		currMonster.setCharge(charge);
	}
}