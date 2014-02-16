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
	public float endY;
	public float endX;
	
	public AudioSource audio2;
	
	private float percentComplete = 0;
    
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
				timeBetweenSwaps = 0;
				maxSwapTime = 0.5f;
				spriter.color = Color.grey;
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
		} else if (!firstShown) {
			bool spriteSwitch = PlayerPrefs.GetInt("Adversary") == 0;
			Monster currMonster = monster.GetComponent<Monster>();
			switch (currMonster.type) {
			case Monster.MonsterType.skeleton:
				if (spriteSwitch) {
					PlayClipAudio2("Speech/Jane/JaneSkele1");
				} else {
					PlayClipAudio2("Speech/John/JohnSkele1");
				}
				break;
			case Monster.MonsterType.orc:
				if (spriteSwitch) {
					PlayClipAudio2("Speech/Jane/JaneOrc1");
				} else {
					PlayClipAudio2("Speech/John/JohnOrc1");
				}
				break;
			case Monster.MonsterType.dragon:
				if (spriteSwitch) {
					PlayClipAudio2("Speech/Jane/JaneDragon1");
				} else {
					PlayClipAudio2("Speech/John/JohnDragon1");
				}
				break;
			case Monster.MonsterType.lich:
				if (spriteSwitch) {
					PlayClipAudio2("Speech/Jane/JaneLich1");
				} else {
					PlayClipAudio2("Speech/John/JohnLich1");
				}
				break;
			case Monster.MonsterType.kraken:
				Social.ReportProgress("Artful.Kraken.Unleashed",100.0, success => {
					Debug.Log(success ? "Reported kraken achievement successfully" : "Failed to report achievement");
				});
				if (spriteSwitch) {
					PlayClip("Speech/Jane/JaneKraken1");
				} else {
					PlayClip("Speech/John/JohnKraken1");
				}
				break;
			}
			firstShown = true;
			button.renderer.enabled = true;
			button.SetActive (true);
		}
		if (firstShown) {
			if (button.transform.position.y != endY) {
				percentComplete += 0.001f;
				Vector3 anchor = button.transform.position;
				Vector3 goal = new Vector3();
				goal.x = button.transform.position.x;
				goal.y = endY;
				goal.z = button.transform.position.z;
				button.transform.position = Vector3.Lerp(anchor, goal, percentComplete);
			} else 	if (button.transform.position.x != endX) {
				percentComplete += 0.001f;
				Vector3 anchor = button.transform.position;
				Vector3 goal = new Vector3();
				goal.x = endX;
				goal.y = button.transform.position.y;
				goal.z = button.transform.position.z;
				button.transform.position = Vector3.Lerp(anchor, goal, percentComplete);
			}
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

	public void PlayClipAudio2(string clipName) {
		audio2.Stop();
		audio2.Pause();
		audio2.clip = null;
		audio2.clip = Resources.Load(clipName)as AudioClip;
		audio2.Play();
	}
}
