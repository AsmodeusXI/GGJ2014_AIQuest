using UnityEngine;
using System.Collections;
using System;

public class AdversaryStats : MonoBehaviour {

	public int totalKills  = 0;
	private int maxIntensity = 0;
	public bool bossInQ = false;
	public bool bossDefeated = false;

	public int numberOfPlayers = 1;
	public int currentTarget = 0;

	//PLAYER 1
	private float moodPlayer1 = 50;
	private int player1Lvl= 1;
	public int totalPlayer1Kills  = 0;
	private int skeletonPlayer1Kills = 0;
	private int orcPlayer1Kills = 0;
	private int lichPlayer1Kills = 0;
	private int dragonPlayer1Kills = 0;
	private int krakenPlayer1Kills = 0;
	private int skeletonsInPlayer1Q;
	private int orcsInPlayer1Q;
	private int dragonsInPlayer1Q;
	private int lichInPlayer1Q;
	private int krakenInPlayer1Q;
	private int inPlayer1QAtMonsterTimeStart;
	public bool gameOverPlayer1;
	private float timeBetweenPlayer1Spawns;
	public GameObject adversary1Emotions;
	public GameObject adversary1TouchArea;
	public GameObject adversary1Indicator;
	private GUIText a1Text;

	//PLAYER 2
	private float moodPlayer2 = 50;
	private int player2Lvl= 1;
	public int totalPlayer2Kills  = 0;
	private int skeletonPlayer2Kills = 0;
	private int orcPlayer2Kills = 0;
	private int lichPlayer2Kills = 0;
	private int dragonPlayer2Kills = 0;
	private int krakenPlayer2Kills = 0;
	private int skeletonsInPlayer2Q;
	private int orcsInPlayer2Q;
	private int dragonsInPlayer2Q;
	private int lichInPlayer2Q;
	private int krakenInPlayer2Q;
	private int inPlayer2QAtMonsterTimeStart;
	public bool gameOverPlayer2;
	private float timeBetweenPlayer2Spawns;
	public GameObject adversary2TouchArea;
	public GameObject adversary2Indicator;
	private GUIText a2Text;


	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay;
	public float shake_intensity;
	public Transform camera;
	public bool monsterMode;
	public int monsterModeSpawned;
	private float monsterModeTimeLimit = 3;
	public GameObject mmLabel;
	private GUIText mmText;
	public GameObject mmTimeLabel;
	private GUIText mmTimeText;
	private int lastLevel;
	public Monster.MonsterType monsterModeType;
	private float monsterModeTimer;
	private Color mmColor;
	private bool touched;
	private RaycastHit hit;


	void Start() {
		numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
		if (numberOfPlayers == 1) {
			adversary1Emotions.transform.position = new Vector3(-4.07f, -0.89f, 18f);
			adversary1Emotions.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		}
		monsterMode = false;
		monsterModeType = Monster.MonsterType.skeleton;
		originPosition = camera.position;
		originRotation = camera.rotation;
		shake_decay = 0.002f;

		monsterModeTimer = Math.Max(4, UnityEngine.Random.Range (4 / player1Lvl, 4 / player1Lvl));
		mmText = (GUIText)mmLabel.GetComponent<GUIText> ();
		mmTimeText = (GUIText)mmTimeLabel.GetComponent<GUIText> ();
		a1Text = (GUIText)adversary1Indicator.GetComponent<GUIText> ();
		a2Text = (GUIText)adversary2Indicator.GetComponent<GUIText> ();


		mmColor = mmText.color;
		mmTimeText.color = Color.clear;
		mmText.color = Color.clear;
		a1Text.color = numberOfPlayers == 0 ? Color.clear : mmColor;
		a2Text.color = Color.clear;
	}
 
	// Update is called once per frame
	void Update () {
		if (numberOfPlayers != 0) {
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
					checkTouches ();
			} else {
					checkMouseActions ();
					checkKeyboardActions();
			}
		}
		if (shake_intensity > 0){
			shakeScreen();
		}
		if (gameOverPlayer1 || gameOverPlayer2 || bossDefeated) return;
		timeBetweenPlayer1Spawns += Time.deltaTime;
		timeBetweenPlayer2Spawns += Time.deltaTime;

		if (totalKills > 115) {
			checkMonsterMode();
		}
		
		if (moodPlayer1 <= 0 || moodPlayer2 <= 0) {
			gameOverPlayer1 = true;
			gameOverPlayer2 = true;
			goToGameOver (3);
		}
		if (totalKills > 0) {
			moodPlayer1 = moodPlayer1 - (((4f * (0.5f + timeBetweenPlayer1Spawns)) * (Time.deltaTime)) * (monsterMode ? 3f : 1f));
			if (numberOfPlayers > 0) {
				moodPlayer2 = moodPlayer2 - (((4f * (0.5f + timeBetweenPlayer1Spawns)) * (Time.deltaTime)) * (monsterMode ? 3f : 1f));
			}
		} else {
			moodPlayer1 = moodPlayer1 - ((0.5f + timeBetweenPlayer1Spawns) * Time.deltaTime);
			if (numberOfPlayers > 0) {
				moodPlayer2 = moodPlayer2 - ((0.5f + timeBetweenPlayer1Spawns) * Time.deltaTime);
			}
		}
		checkMusic();
	}

	private void checkTouches() {
		for (var i = 0; i < Input.touchCount; ++i) {
			Touch touch = Input.GetTouch (i);
			Ray ray = Camera.main.ScreenPointToRay (touch.position);
			if (Physics.Raycast (ray, out hit, 100.0f)) {
				if (hit.collider.name == adversary1TouchArea.name) {
					a1Text.color = mmColor;
					a2Text.color = Color.clear;
					currentTarget = 0;
				} else if (hit.collider.name == adversary2TouchArea.name) {
					a2Text.color = mmColor;
					a1Text.color = Color.clear;
					currentTarget = 1;
				}
			} 
		}
	}

	private void checkMouseActions() {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 touchPos = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay (touchPos);
			if (Physics.Raycast (ray, out hit, 100.0f)) {
				if (hit.collider.name == adversary1TouchArea.name) {
					a1Text.color = mmColor;
					a2Text.color = Color.clear;
					currentTarget = 0;
				} else if (hit.collider.name == adversary2TouchArea.name) {
					a2Text.color = mmColor;
					a1Text.color = Color.clear;
					currentTarget = 1;
				}
			}
		} 
	}

	private void checkKeyboardActions() {
		if (Input.GetKeyUp (KeyCode.Space)) {
			if (currentTarget == 0) {
				a2Text.color = mmColor;
				a1Text.color = Color.clear;
				currentTarget = 1;
			} else {
				a1Text.color = mmColor;
				a2Text.color = Color.clear;
				currentTarget = 0;
			}
		}
	}

	void checkMonsterMode() {
		if (monsterModeTimer <= 0 && !monsterMode) {
				mmText.color = Color.clear;
				mmTimeText.color = Color.clear;
				monsterModeTimer = Math.Max(10, UnityEngine.Random.Range (4 / player1Lvl, 4 / player1Lvl));
				monsterMode = true;
			if (currentTarget == 0) {
				inPlayer1QAtMonsterTimeStart = monsterTypePlayer1Queued(monsterModeType);
			} else {
				inPlayer2QAtMonsterTimeStart = monsterTypePlayer2Queued(monsterModeType);
			}
		} else if (!monsterMode) {
			if (monsterModeTimer < 5) {
				mmText.text = stringForMonsterType(monsterModeType) + " FRENZY IN: ";
				mmTimeText.text = "" + Mathf.FloorToInt(monsterModeTimer);
				mmTimeText.color = mmColor;
				mmText.color = mmColor;
			}
			monsterModeTimer -= Time.deltaTime;
		} else {
			monsterModeTimeLimit -= Time.deltaTime;
			if (monsterModeTimeLimit < 0) {
				monsterModeSpawned = 0;
				monsterModeTimeLimit = 3;
			}
		}

	}

	void shakeScreen() {
		camera.position = originPosition + UnityEngine.Random.insideUnitSphere * shake_intensity;
		camera.rotation = new Quaternion(
			originRotation.x + UnityEngine.Random.Range (-shake_intensity,shake_intensity) * .2f,
			originRotation.y + UnityEngine.Random.Range (-shake_intensity,shake_intensity) * .2f,
			originRotation.z + UnityEngine.Random.Range (-shake_intensity,shake_intensity) * .2f,
			originRotation.w + UnityEngine.Random.Range (-shake_intensity,shake_intensity) * .2f);
		shake_intensity -= shake_decay;
		if (shake_intensity <= 0) {
			camera.position = originPosition;
			camera.rotation = originRotation;
		}
	}

	void shake(int intensity, bool affectMax){
		shake_intensity += intensity * 0.002f;
		if (shake_intensity * 100 > maxIntensity && affectMax) {
			maxIntensity = Mathf.FloorToInt(shake_intensity * 100);
			if (maxIntensity > 100) {
				Social.ReportProgress("Artful.Max.Intensity",100.0, success => {
					Debug.Log(success ? "Reported Boss Battle achievement successfully" : "Failed to report achievement");
				});
			}
		}
	}

	public void checkMusic() {
		if (moodPlayer1 > (.65 * (player1Lvl*100)) && !audio.clip.name.Equals("FrusteratedPlayerState")) {
			PlayClip("Music/FrusteratedPlayerState");
		} else if (moodPlayer1 < (.35*(player1Lvl*100)) && !audio.clip.name.Equals("BoredPlayerState")) {
			PlayClip("Music/BoredPlayerState");
		} else if ((moodPlayer1 < (.65 * (player1Lvl*100)) && moodPlayer1 > (.35*(player1Lvl*100))) && !audio.clip.name.Equals("NormalPlayerStateRevamped")) {
			PlayClip ("Music/NormalPlayerStateRevamped");
		} 
	}

	public void PlayClip(string clipName){
		audio.Stop();
		audio.Pause();
		audio.clip = null;
		audio.clip = Resources.Load(clipName)as AudioClip;
		audio.Play();
	}
	//goes to new scene where the game could be restarted or quit out of
	public void goToGameOver(int waitTime) {
		string hardScore = numberOfPlayers == 0 ? "" : ".Hard";
		PlayerPrefs.SetInt("Artful.Total.Spawns" + hardScore, totalKills);
		PlayerPrefs.SetInt("Artful.Skeleton.Spawns" + hardScore, skeletonPlayer1Kills + skeletonPlayer2Kills);
		PlayerPrefs.SetInt("Artful.Orc.Spawns" + hardScore, orcPlayer1Kills + orcPlayer2Kills);
		PlayerPrefs.SetInt("Artful.Dragon.Spawns" + hardScore, dragonPlayer1Kills + dragonPlayer2Kills);
		PlayerPrefs.SetInt("Artful.Lich.Spawns" + hardScore, lichPlayer1Kills + lichPlayer2Kills);
		PlayerPrefs.SetInt("Artful.Kraken.Spawns" + hardScore, krakenPlayer1Kills + krakenPlayer2Kills);
		PlayerPrefs.SetInt("Artful.Max.Intensity", maxIntensity);
		StartCoroutine(Wait (waitTime));
	}
	
	IEnumerator Wait(int seconds) {
		yield return new WaitForSeconds(seconds);
		Application.LoadLevel("ScoreScene");
	}

	public int getCurrentPlayer() {
		return currentTarget;
	}

	public float getMood(int target) {
		if (target == 0) {
			return moodPlayer1;
		} else {
			return moodPlayer2;
		}
	}

	public float getRelativeMood(int target) {
		if (target == 0) {
			return (moodPlayer1 / (player1Lvl * 100)) * 100;
		} else {
			return (moodPlayer2 / (player2Lvl * 100)) * 100;
		}
	}

	// Describes how a monster harms/improves the player when affecteds
	public void monsterAffects(Monster monster) {
		if (monster.type == Monster.MonsterType.boss) {
			finalBattle();
			return;
		}
		if (gameOverPlayer1 || gameOverPlayer2 || bossDefeated) return;
		if (monster.target == 0) {
			float nextMood = moodPlayer1 + (((int)monster.type + 1) * monster.getLevel ()) * 3;
			if (nextMood >= (100 * player1Lvl)) {
				moodPlayer1 += (((int)monster.type + 1) * monster.getLevel ()) * 3;
				shake ((((int)monster.type + 1) * monster.getLevel ()) * 3, false);
				gameOverPlayer1 = true;
				goToGameOver (3);
				return;
			}
		} else {
			float nextMood = moodPlayer2 + (((int)monster.type + 1) * monster.getLevel ()) * 3;
			if (nextMood >= (100 * player2Lvl)) {
				moodPlayer2 += (((int)monster.type + 1) * monster.getLevel ()) * 3;
				shake ((((int)monster.type + 1) * monster.getLevel ()) * 3, false);
				gameOverPlayer1 = true;
				goToGameOver (3);
				return;
			}
		}
		if (monsterMode) {
			if (monster.type == monsterModeType && inPlayer1QAtMonsterTimeStart <=0) {
				monsterModeSpawned++;
				if (monsterModeSpawned > 10) {
					monsterModeSpawned = 0;
					monsterModeTimeLimit = 3;
					monsterMode = false;
					float rannum = UnityEngine.Random.Range(0f, 1f) * Math.Min(player1Lvl, 4);
					monsterModeType = (Monster.MonsterType)(System.Math.Round(rannum));
				}
			} else {
				if (currentTarget == 0) {
					inPlayer1QAtMonsterTimeStart--;
				} else {
					inPlayer2QAtMonsterTimeStart--;
				}
			}
		}
		checkMusic();
		shake ((((int)monster.type + 1) * monster.getLevel()) * 3, true);
		if (monster.target == 0) {
			timeBetweenPlayer1Spawns = 0;
			moodPlayer1 += (((int)monster.type + 1) * monster.getLevel ()) * 3;
		} else {
			timeBetweenPlayer2Spawns = 0;
			moodPlayer2 += (((int)monster.type + 1) * monster.getLevel ()) * 3;
		}
		incrementMonsterKills (monster);
		totalKills++;
		if (monster.target == 0) {
			totalPlayer1Kills++;
			player1Lvl = Math.Max (1, totalPlayer1Kills / 50);
		} else {
			totalPlayer2Kills++;
			player2Lvl = Math.Max (1, totalPlayer2Kills / 50);
		}
	}

	private void finalBattle () {
		if ((35 < getRelativeMood (0) && getRelativeMood(0) < 65) && (35 < getRelativeMood (1) && getRelativeMood(1) < 65)) {
			Debug.Log ("WIN!!!!!!");
			bossDefeated = true;
			PlayerPrefs.SetInt ("hardEnabled", 1);
			shake(300, false);
			goToGameOver(10);
		} else {
			moodPlayer1 = 200 * player1Lvl;
			moodPlayer2 = 200 * player2Lvl;
			gameOverPlayer1 = true;
			gameOverPlayer2 = true;
			goToGameOver(3);
		}
	}

	public bool tooLowLevel(Monster monster) {
		if (bossInQ) {
			return true;
		}
		if (monsterMode) {
				if (monster.type == monsterModeType) {
					return false;
				} else {
					return true;
				}
		} else {
			return (monsterKills (monster) + monsterQueued (monster) > monster.getLevel () * 10);
		}
	}

	private int monsterKills(Monster monster) {
		if (monster.target == 0) {
			switch (monster.type) {
			case Monster.MonsterType.skeleton:
					return skeletonPlayer1Kills;
			case Monster.MonsterType.orc:
					return orcPlayer1Kills;
			case Monster.MonsterType.dragon:
					return dragonPlayer1Kills;
			case Monster.MonsterType.lich:
					return lichPlayer1Kills;
			case Monster.MonsterType.kraken:
					return krakenPlayer1Kills;
			}
			return 0;
		} else {
			switch (monster.type) {
			case Monster.MonsterType.skeleton:
				return skeletonPlayer2Kills;
			case Monster.MonsterType.orc:
				return orcPlayer2Kills;
			case Monster.MonsterType.dragon:
				return dragonPlayer2Kills;
			case Monster.MonsterType.lich:
				return lichPlayer2Kills;
			case Monster.MonsterType.kraken:
				return krakenPlayer2Kills;
			}
			return 0;
		}
	}
	
	public int monsterQueued(Monster monster) {

		if (monster.target == 0) {
			return monsterTypePlayer1Queued (monster.type);
		} else {
			return monsterTypePlayer2Queued (monster.type);
		}
	}

	private int monsterTypePlayer1Queued(Monster.MonsterType monsterType) {
		switch (monsterType) {
		case Monster.MonsterType.skeleton:
			return skeletonsInPlayer1Q;
		case Monster.MonsterType.orc:
			return orcsInPlayer1Q;
		case Monster.MonsterType.dragon:
			return dragonsInPlayer1Q;
		case Monster.MonsterType.lich:
			return lichInPlayer1Q;
		case Monster.MonsterType.kraken:
			return krakenInPlayer1Q;
		}
		return 0;
	}

	private int monsterTypePlayer2Queued(Monster.MonsterType monsterType) {
		switch (monsterType) {
		case Monster.MonsterType.skeleton:
			return skeletonsInPlayer2Q;
		case Monster.MonsterType.orc:
			return orcsInPlayer2Q;
		case Monster.MonsterType.dragon:
			return dragonsInPlayer2Q;
		case Monster.MonsterType.lich:
			return lichInPlayer2Q;
		case Monster.MonsterType.kraken:
			return krakenInPlayer2Q;
		}
		return 0;
	}

	public void incrementMonsterKills(Monster monster) {
		if (monster.target == 0) {
			incrementPlayer1MonsterKills(monster);
		} else {
			incrementPlayer2MonsterKills(monster);
		}
	}

	private void incrementPlayer1MonsterKills(Monster monster) {
		switch (monster.type) {
		case Monster.MonsterType.skeleton:
			skeletonPlayer1Kills++;
			break;
		case Monster.MonsterType.orc:
			orcPlayer1Kills++;
			break;
		case Monster.MonsterType.dragon:
			dragonPlayer1Kills++;
			break;
		case Monster.MonsterType.lich:
			lichPlayer1Kills++;
			break;
		case Monster.MonsterType.kraken:
			krakenPlayer1Kills++;
			break;
		}
	}

	private void incrementPlayer2MonsterKills(Monster monster) {
		switch (monster.type) {
		case Monster.MonsterType.skeleton:
			skeletonPlayer2Kills++;
			break;
		case Monster.MonsterType.orc:
			orcPlayer2Kills++;
			break;
		case Monster.MonsterType.dragon:
			dragonPlayer2Kills++;
			break;
		case Monster.MonsterType.lich:
			lichPlayer2Kills++;
			break;
		case Monster.MonsterType.kraken:
			krakenPlayer2Kills++;
			break;
		}
	}

	public void incrementMonsterQ(Monster monster) {
		if (monster.type == Monster.MonsterType.boss) {
			bossInQ = true;
		}
		if (monster.target == 0) {
			incrementPlayer1MonsterQ(monster);
		} else {
			incrementPlayer2MonsterQ(monster);
		}
	}
	
	private void incrementPlayer1MonsterQ(Monster monster) {
		switch (monster.type) {
		case Monster.MonsterType.skeleton:
			skeletonsInPlayer1Q++;
			break;
		case Monster.MonsterType.orc:
			orcsInPlayer1Q++;
			break;
		case Monster.MonsterType.dragon:
			dragonsInPlayer1Q++;
			break;
		case Monster.MonsterType.lich:
			lichInPlayer1Q++;
			break;
		case Monster.MonsterType.kraken:
			krakenInPlayer1Q++;
			break;
		}
	}

	private void incrementPlayer2MonsterQ(Monster monster) {
		switch (monster.type) {
		case Monster.MonsterType.skeleton:
			skeletonsInPlayer2Q++;
			break;
		case Monster.MonsterType.orc:
			orcsInPlayer2Q++;
			break;
		case Monster.MonsterType.dragon:
			dragonsInPlayer2Q++;
			break;
		case Monster.MonsterType.lich:
			lichInPlayer2Q++;
			break;
		case Monster.MonsterType.kraken:
			krakenInPlayer2Q++;
			break;
		}
	}

	public void decrementMonsterQ(Monster monster) {
		if (monster.target == 0) {
			decrementPlayer1MonsterQ(monster);
		} else {
			decrementPlayer2MonsterQ(monster);
		}
	}
	
	private void decrementPlayer1MonsterQ(Monster monster) {
		switch (monster.type) {
		case Monster.MonsterType.skeleton:
			skeletonsInPlayer1Q--;
			break;
		case Monster.MonsterType.orc:
			orcsInPlayer1Q--;
			break;
		case Monster.MonsterType.dragon:
			dragonsInPlayer1Q--;
			break;
		case Monster.MonsterType.lich:
			lichInPlayer1Q--;
			break;
		case Monster.MonsterType.kraken:
			krakenInPlayer1Q--;
			break;
		}
	}

	private void decrementPlayer2MonsterQ(Monster monster) {
		switch (monster.type) {
		case Monster.MonsterType.skeleton:
			skeletonsInPlayer2Q--;
			break;
		case Monster.MonsterType.orc:
			orcsInPlayer2Q--;
			break;
		case Monster.MonsterType.dragon:
			dragonsInPlayer2Q--;
			break;
		case Monster.MonsterType.lich:
			lichInPlayer2Q--;
			break;
		case Monster.MonsterType.kraken:
			krakenInPlayer2Q--;
			break;
		}
	}

	private string stringForMonsterType(Monster.MonsterType monsterType) {
		switch (monsterType) {
		case Monster.MonsterType.skeleton:
			return "Skeleton";
		case Monster.MonsterType.orc:
			return "Orc";
		case Monster.MonsterType.dragon:
			return "Dragon";
		case Monster.MonsterType.lich:
			return "Lich";
		case Monster.MonsterType.kraken:
			return "Kraken";
		}
		return "";
	}
}