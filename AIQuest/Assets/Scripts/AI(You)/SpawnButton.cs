using UnityEngine;
using System.Collections;
using System;

public class SpawnButton : MonoBehaviour {

	private float chargeLevel;
	private bool timerOn;
	public Transform monster;
    public string keyboardInput;
    public GameObject button;
    private SpriteRenderer spriter;
    private float chargeMarker = 0.5f;
    private bool colorSwitch;
    
    KeyCode keyboardButton;
    
    private Transform buttonPosition;
	void Start () {
        buttonPosition = GetComponent<Transform>();    
        keyboardButton = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardInput.ToUpper());
        spriter = (SpriteRenderer) button.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(keyboardButton) ){
			timerOn = true;
        }
        
		if (timerOn) {
			chargeLevel += Time.deltaTime;
			
			if (chargeLevel >= chargeMarker) {
				colorSwitch = !colorSwitch;
				chargeMarker += chargeMarker;
			}
			
			if(colorSwitch) {
				spriter.color = Color.yellow;
			} else {
				spriter.color = Color.white;
			}
		}
		
		if (Input.GetKeyUp (keyboardButton)) {
			Spawn (chargeLevel);
			chargeLevel = 0;
			chargeMarker = 0;
			timerOn = false;
			spriter.color = Color.white;
		}
	}
	
	void OnMouseDown(){
		timerOn = true;
		spriter.color = Color.red;
	}

	void OnMouseUp(){
		Spawn (chargeLevel);
		chargeLevel = 0;
		timerOn = false;
		spriter.color = Color.white;
	}
	
	void Spawn(float charge){
		Transform temp = (Transform)Instantiate(monster, buttonPosition.position, Quaternion.identity);
		Monster currMonster = temp.GetComponent<Monster>();
		currMonster.setCharge(charge);
	}
}
