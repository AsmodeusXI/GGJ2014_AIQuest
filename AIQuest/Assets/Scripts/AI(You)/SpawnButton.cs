﻿using UnityEngine;
using System.Collections;
using System;

public class SpawnButton : MonoBehaviour {

	private float chargeLevel;
	private bool timerOn;
	public Transform monster;
    public string keyboardInput;
    public GameObject button;
    private SpriteRenderer spriter;
    private bool buttonLit;
	private bool firstRed;
    private float timeBetweenSwaps;
    private float maxSwapTime = 0.5f;
    
    KeyCode keyboardButton;
    
    private Transform buttonPosition;
	void Start () {
        buttonPosition = GetComponent<Transform>();    
        keyboardButton = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardInput.ToUpper());
        spriter = (SpriteRenderer) button.GetComponent<SpriteRenderer>();
    }
    
	void OnMouseDown(){
		timerOn = true;
	}
	
	void OnMouseUp(){
		Spawn (chargeLevel);
		chargeLevel = 0;
		timerOn = false;
		spriter.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(keyboardButton) ){
			timerOn = true;
        }
        
		if (timerOn) {
			chargeLevel += Time.deltaTime;
			flashingColors();	
		}
		
		if (Input.GetKeyUp (keyboardButton)) {
			Spawn (chargeLevel);
			chargeLevel = 0;
			timeBetweenSwaps = 0;
			maxSwapTime = 0.5f;
			firstRed = false;
			timerOn = false;
			spriter.color = Color.white;
		}
	}
	
	void Spawn(float charge){
		Transform temp = (Transform)Instantiate(monster, buttonPosition.position, Quaternion.identity);
		Monster currMonster = temp.GetComponent<Monster>();
		currMonster.setCharge(charge);
	}
	
	void flashingColors(){
	
		timeBetweenSwaps += Time.deltaTime;
		
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		Monster currMonster = monster.GetComponent<Monster>();
		currMonster.setCharge(chargeLevel);	
		
		if(!advStats.tooLowLevel(currMonster)) {
			if (!firstRed) {
				firstRed = true;
				maxSwapTime = 0.5f;
				spriter.color = Color.red;
				return;
			}
			if(maxSwapTime <= timeBetweenSwaps && buttonLit == false){
				spriter.color = Color.red;
				timeBetweenSwaps = 0;
				buttonLit = true;
				maxSwapTime = Math.Max(maxSwapTime - 0.05f, 0.05f);
			}
			else if(maxSwapTime <= timeBetweenSwaps && buttonLit == true){
				spriter.color = Color.white;
				timeBetweenSwaps = 0;
				buttonLit = false;
				maxSwapTime = Math.Max(maxSwapTime - 0.05f, 0.05f);
			}
		}
		else if(maxSwapTime <= timeBetweenSwaps && buttonLit == false){
			spriter.color = Color.yellow;
			timeBetweenSwaps = 0;
			buttonLit = true;
			maxSwapTime = Math.Max(maxSwapTime - 0.05f, 0.05f);
		}
		else if(maxSwapTime <= timeBetweenSwaps && buttonLit == true){
			spriter.color = Color.white;
			timeBetweenSwaps = 0;
			buttonLit = false;
			maxSwapTime = Math.Max(maxSwapTime - 0.05f, 0.05f);
		}
	}
}
