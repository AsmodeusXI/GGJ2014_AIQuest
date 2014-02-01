using UnityEngine;
using System.Collections;
using System;

public class SpawnButton : MonoBehaviour {

	private float chargeLevel;
	private bool timerOn;
    public GameObject lvlIndicator;
    private MonsterLevelIndicator lvlSign;
	public Transform monster;
    public string keyboardInput;
	public string buttonInput;
    public GameObject button;
//	public Transform particleEffect;
    private SpriteRenderer spriter;
    private bool buttonLit;
	private bool firstRed;
    private float timeBetweenSwaps;
    private float maxSwapTime = 0.5f;
	public int minimumSpawnToShow;
    
    KeyCode keyboardButton;

    private int lastLevel;

    private Transform buttonPosition;
	void Start () {
//		particleEffect.particleSystem.renderer.sortingLayerName = "Foreground";
        buttonPosition = GetComponent<Transform>();    
        keyboardButton = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardInput.ToUpper());
        spriter = (SpriteRenderer) button.GetComponent<SpriteRenderer>();
        lvlSign = lvlIndicator.GetComponent<MonsterLevelIndicator>();
    }

    void OnMouseDown(){
        timerOn = true;
        lvlSign.unhide();
        lastLevel = 0;
	}
	
    void OnMouseUp(){
		lvlSign.hide();
		Spawn (chargeLevel);
		chargeLevel = 0;
		timeBetweenSwaps = 0;
		maxSwapTime = 0.5f;
		firstRed = false;
		timerOn = false;
		spriter.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		if (advStats.totalKills < minimumSpawnToShow) {
			button.SetActive (false);
			button.renderer.enabled = false;
			return;
		} else {
			button.renderer.enabled = true;
			button.SetActive (true);
		}

		if(Input.GetKeyDown(keyboardButton)){
            lvlSign.increment();
			timerOn = true;
            lastLevel = 0;
        }
        
		if (timerOn) {
			chargeLevel += Time.deltaTime;
			flashingColors();	
            
            int effectiveLevel = Mathf.FloorToInt(chargeLevel);
            if(effectiveLevel != lastLevel)
            {
                lastLevel = effectiveLevel;
                lvlSign.increment();
            }
		}
		
		if (Input.GetKeyUp (keyboardButton)) {
            lvlSign.hide();
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
//				if (particleEffect != null) {
//					Vector3 vector = new Vector3();
//					vector.x = lvlIndicator.transform.position.x;
//					vector.y = lvlIndicator.transform.position.y;
//					vector.z = lvlIndicator.transform.position.z - 10;
//					Instantiate(particleEffect, vector, Quaternion.identity);
//				}
				firstRed = true;
				maxSwapTime = 0.5f;
				spriter.color = Color.red;
				switch (currMonster.type) {
				case Monster.MonsterType.skeleton:
					PlayClip("SoundFX/Skeleton");
					break;
				case Monster.MonsterType.orc:
					PlayClip("SoundFX/Orc");
					break;
				case Monster.MonsterType.dragon:
					PlayClip("SoundFX/Dragon");
					break;
				case Monster.MonsterType.lich:
					PlayClip("SoundFX/Lich");
					break;
				}
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
	
	public void PlayClip(string clipName){
		audio.Stop();
		audio.Pause();
		audio.clip = null;
		audio.clip = Resources.Load(clipName)as AudioClip;
		audio.Play();
	}
}
