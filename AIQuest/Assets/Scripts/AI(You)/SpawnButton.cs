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
	public GameObject collideButton;
    private SpriteRenderer spriter;
	private SpriteRenderer lvlSpriter;
    private bool buttonLit;
	private bool firstRed;
    private float timeBetweenSwaps;
    private float maxSwapTime = 0.5f;
	public int minimumSpawnToShow;
	private bool firstShown;
	private AdversaryStats advStats;
	private RaycastHit hit;
	private bool touched;
    
    KeyCode keyboardButton;

    private int lastLevel;

    private Transform buttonPosition;
	void Start () {
        buttonPosition = GetComponent<Transform>();    
        keyboardButton = (KeyCode)Enum.Parse(typeof(KeyCode), keyboardInput.ToUpper());
        spriter = (SpriteRenderer) button.GetComponent<SpriteRenderer>();
        lvlSign = lvlIndicator.GetComponent<MonsterLevelIndicator>();
		lvlSpriter = (SpriteRenderer)lvlIndicator.GetComponent<SpriteRenderer> ();
		lvlSpriter.color = Color.clear;
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
    }

	// Update is called once per frame
	void Update () {
		bool returnFast = checkToUnlockButton ();
		if (!returnFast) return;
		Monster currMonster = monster.GetComponent<Monster> ();
		checkMonsterMode ();
		if (advStats.monsterMode && currMonster.type != advStats.monsterModeType) return;


		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) 
		{
			checkTouches();
		} else {
			checkMouseActions();
		}
		
		if(Input.GetKeyDown(keyboardButton)){
			downAction();
        }
        
		checkChargingActions();
		
		if (Input.GetKeyUp (keyboardButton)) {
			upAction ();
		}
	}

	private void checkMonsterMode() {
		if (advStats.monsterMode) {
			Monster currMonster = monster.GetComponent<Monster> ();
			if (currMonster.type == advStats.monsterModeType) {
				spriter.color = Color.red;
			} else {
				lvlSign.hide();
				chargeLevel = 0;
				timeBetweenSwaps = 0;
				maxSwapTime = 0.5f;
				firstRed = false;
				timerOn = false;
				lvlSpriter.color = Color.clear;
				spriter.color = Color.clear;
			}
		} else if (!timerOn) {
			spriter.color = Color.white;
		}
	}

	private void checkTouches() {
		bool touchedThisTime = false;
		for (var i = 0; i < Input.touchCount; ++i) {
			Touch touch = Input.GetTouch (i);
			Ray ray = Camera.main.ScreenPointToRay (touch.position);
			if (Physics.Raycast (ray, out hit, 100.0f)) {
					if (hit.collider.name == collideButton.name) {
						touchedThisTime = true;
						if(!touched) {
							touched = true;
							downAction ();
						}
					}
			} 
		}
		if (!touchedThisTime && touched) {
			upAction();
			touched = false;
		}

	}

	private void checkMouseActions() {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 touchPos = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay (touchPos);
			if (Physics.Raycast (ray, out hit, 100.0f)) {
				if (hit.collider.name == collideButton.name) {
					downAction ();
				}
			}
		} else if (Input.GetMouseButtonUp(0)) {
			Vector3 touchPos = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay (touchPos);
			if (Physics.Raycast (ray, out hit, 100.0f)) {
				if (hit.collider.name == collideButton.name) {
					upAction ();
				}
			}
		}
	}
	
	private void checkChargingActions() {
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
	}

	private bool checkToUnlockButton() {
		if (advStats.totalKills < minimumSpawnToShow) {
			button.SetActive (false);
			button.renderer.enabled = false;
			return false;
		} else if (!firstShown && minimumSpawnToShow > 0) {
			Monster currMonster = monster.GetComponent<Monster>();
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
			case Monster.MonsterType.kraken:
				PlayClip("SoundFX/Kraken2");
				break;
			}
			firstShown = true;
			button.renderer.enabled = true;
			button.SetActive (true);
		}
		return true;
	}
	
	private void downAction() {
		lvlSign.increment();
		timerOn = true;
		lastLevel = 0;
	}
	
	private void upAction() {
		lvlSign.hide();
		
		Monster currMonster = monster.GetComponent<Monster>();
		currMonster.setCharge(chargeLevel);	
		if(!advStats.tooLowLevel(currMonster)) {
			Spawn (chargeLevel);
		}
		chargeLevel = 0;
		timeBetweenSwaps = 0;
		maxSwapTime = 0.5f;
		firstRed = false;
		timerOn = false;
		spriter.color = Color.white;
		lvlSpriter.color = Color.clear;
	}
	
	void Spawn(float charge){
		Transform temp = (Transform)Instantiate(monster, buttonPosition.position, Quaternion.identity);
		Monster currMonster = temp.GetComponent<Monster>();
		currMonster.setCharge(charge);
	}
	
	void flashingColors(){
		
		timeBetweenSwaps += Time.deltaTime;

		Monster currMonster = monster.GetComponent<Monster>();
		currMonster.setCharge(chargeLevel);	
		
		if(!advStats.tooLowLevel(currMonster)) {
			if (!firstRed) {
				firstRed = true;
				maxSwapTime = 0.5f;
				spriter.color = Color.red;
				lvlSpriter.color = Color.white;
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
				case Monster.MonsterType.kraken:
					PlayClip("SoundFX/Kraken");
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
