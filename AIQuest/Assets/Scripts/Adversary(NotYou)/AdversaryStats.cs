using UnityEngine;
using System.Collections;
using System;

public class AdversaryStats : MonoBehaviour {

	private float mood = 50;
	private int yourLvl= 1;
	public int totalKills  = 0;
	private int skeletonKills = 0;
	private int orcKills = 0;
	private int lichKills = 0;
	private int dragonKills = 0;
	private int krakenKills = 0;
	private int maxIntensity = 0;
	private int skeletonsInQ;
	private int orcsInQ;
	private int dragonsInQ;
	private int lichInQ;
	private int krakenInQ;
	private int inQAtMonsterTimeStart;
	private bool gameOver;
	private float timeBetweenSpawns;
	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay;
	public float shake_intensity;
	public Transform camera;
	public bool monsterMode;
	public GameObject mmIndicator;
	private MonsterLevelIndicator mmSign;
	private SpriteRenderer mmSpriter;
	private int lastLevel;
	public Monster.MonsterType monsterModeType;
	private float monsterModeTimer;


	void Start() {
		monsterMode = false;
		monsterModeType = Monster.MonsterType.skeleton;
		originPosition = camera.position;
		originRotation = camera.rotation;
		shake_decay = 0.002f;

		monsterModeTimer = Math.Max(4, UnityEngine.Random.Range (4 / yourLvl, 4 / yourLvl));
		mmSign = mmIndicator.GetComponent<MonsterLevelIndicator>();
		mmSpriter = (SpriteRenderer)mmIndicator.GetComponent<SpriteRenderer> ();
		mmSpriter.color = Color.white;
	}
 
	// Update is called once per frame
	void Update () {
		if (shake_intensity > 0){
			shakeScreen();
		}
		if (gameOver) return;
		timeBetweenSpawns += Time.deltaTime;

		if (totalKills > 115) {
			checkMonsterMode();
		}
		
		if (mood <= 0) {
			gameOver = true;
			goToGameOver ();
		}
		if (totalKills > 0) {
			mood = mood - (((4f * (0.5f + timeBetweenSpawns)) * (Time.deltaTime)) * (monsterMode ? 3f : 1f));
		} else {
			mood = mood - ((0.5f + timeBetweenSpawns) * Time.deltaTime);
		}
		checkMusic();
	}

	void checkMonsterMode() {
		if (monsterModeTimer <= 0 && !monsterMode) {
				monsterModeTimer = Math.Max(4, UnityEngine.Random.Range (4 / yourLvl, 4 / yourLvl));
				monsterMode = true;
				float rannum = UnityEngine.Random.Range(0f, 1f) * Math.Min(yourLvl, 4);
				monsterModeType = (Monster.MonsterType)(System.Math.Round(rannum));
				inQAtMonsterTimeStart = monsterTypeQueued(monsterModeType);
	//			playClipForMonsterType(monsterModeType);
				mmSign.hide();
		} else if (!monsterMode) {
			if (monsterModeTimer < 3) {
				mmSignCheck();
			}
			monsterModeTimer -= Time.deltaTime;
		}

	}

	private void mmSignCheck() {
		int effectiveLevel = Mathf.FloorToInt(monsterModeTimer);
		if(effectiveLevel != lastLevel)
		{
			lastLevel = effectiveLevel;
			mmSign.increment();
		}
	}

//	void playClipForMonsterModeType(Monster.MonsterType monsterType) {
//		switch (monsterType) {
//		case Monster.MonsterType.skeleton:
//
//		case Monster.MonsterType.orc:
//
//		case Monster.MonsterType.dragon:
//
//		case Monster.MonsterType.lich:
//
//		case Monster.MonsterType.kraken:
//
//		}
//		return 0;
//	}

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
		if (mood > (.65 * (yourLvl*100)) && !audio.clip.name.Equals("FrusteratedPlayerState")) {
			PlayClip("Music/FrusteratedPlayerState");
		} else if (mood < (.35*(yourLvl*100)) && !audio.clip.name.Equals("BoredPlayerState")) {
			PlayClip("Music/BoredPlayerState");
		} else if ((mood < (.65 * (yourLvl*100)) && mood > (.35*(yourLvl*100))) && !audio.clip.name.Equals("NormalPlayerStateRevamped")) {
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
	public void goToGameOver() {
		PlayerPrefs.SetInt("Artful.Total.Spawns", totalKills);
		PlayerPrefs.SetInt("Artful.Skeleton.Spawns", skeletonKills);
		PlayerPrefs.SetInt("Artful.Orc.Spawns", orcKills);
		PlayerPrefs.SetInt("Artful.Dragon.Spawns", dragonKills);
		PlayerPrefs.SetInt("Artful.Lich.Spawns", lichKills);
		PlayerPrefs.SetInt("Artful.Kraken.Spawns", krakenKills);
		PlayerPrefs.SetInt("Artful.Max.Intensity", maxIntensity);
		StartCoroutine(Wait (3));
	}
	
	IEnumerator Wait(int seconds) {
		yield return new WaitForSeconds(seconds);
		Application.LoadLevel("ScoreScene");
	}

	public float getMood() {
		return mood;
	}

	public float getRelativeMood() {
		return (mood / (yourLvl * 100)) * 100;
	}

	// Describes how a monster harms/improves the player when affecteds
	public void monsterAffects(Monster monster) {
		if (gameOver) return;
		float nextMood = mood + (((int)monster.type + 1) * monster.getLevel()) * 3;
		if (nextMood >= (100 * yourLvl)) {
			mood += (((int)monster.type + 1) * monster.getLevel()) * 3;
			shake ((((int)monster.type + 1) * monster.getLevel()) * 3, false);
			gameOver = true;
			goToGameOver ();
			return;
		}
		if (monsterMode) {
			if (monster.type == monsterModeType && inQAtMonsterTimeStart <=0) {
				monsterMode = false;
			} else {
				inQAtMonsterTimeStart--;
			}
		}

		timeBetweenSpawns = 0;
		checkMusic();
		shake ((((int)monster.type + 1) * monster.getLevel()) * 3, true);
		mood += (((int)monster.type + 1) * monster.getLevel()) * 3;
		incrementMonsterKills (monster);
		totalKills++;
		yourLvl = Math.Max (1, totalKills / 50);
	}

	public bool tooLowLevel(Monster monster) {
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
		switch (monster.type) {
		case Monster.MonsterType.skeleton:
				return skeletonKills;
		case Monster.MonsterType.orc:
				return orcKills;
		case Monster.MonsterType.dragon:
				return dragonKills;
		case Monster.MonsterType.lich:
				return lichKills;
		case Monster.MonsterType.kraken:
			return krakenKills;
		}
		return 0;
	}
	
	private int monsterQueued(Monster monster) {
		return monsterTypeQueued (monster.type);
	}

	private int monsterTypeQueued(Monster.MonsterType monsterType) {
		switch (monsterType) {
		case Monster.MonsterType.skeleton:
			return skeletonsInQ;
		case Monster.MonsterType.orc:
			return orcsInQ;
		case Monster.MonsterType.dragon:
			return dragonsInQ;
		case Monster.MonsterType.lich:
			return lichInQ;
		case Monster.MonsterType.kraken:
			return krakenInQ;
		}
		return 0;
	}

	private void incrementMonsterKills(Monster monster) {
		switch (monster.type) {
		case Monster.MonsterType.skeleton:
			skeletonKills++;
			break;
		case Monster.MonsterType.orc:
			orcKills++;
			break;
		case Monster.MonsterType.dragon:
			dragonKills++;
			break;
		case Monster.MonsterType.lich:
			lichKills++;
			break;
		case Monster.MonsterType.kraken:
			krakenKills++;
			break;
		}
	}
	
	public void incrementMonsterQ(Monster monster) {
		switch (monster.type) {
		case Monster.MonsterType.skeleton:
			skeletonsInQ++;
			break;
		case Monster.MonsterType.orc:
			orcsInQ++;
			break;
		case Monster.MonsterType.dragon:
			dragonsInQ++;
			break;
		case Monster.MonsterType.lich:
			lichInQ++;
			break;
		case Monster.MonsterType.kraken:
			krakenInQ++;
			break;
		}
	}
	
	public void decrementMonsterQ(Monster monster) {
		switch (monster.type) {
		case Monster.MonsterType.skeleton:
			skeletonsInQ--;
			break;
		case Monster.MonsterType.orc:
			orcsInQ--;
			break;
		case Monster.MonsterType.dragon:
			dragonsInQ--;
			break;
		case Monster.MonsterType.lich:
			lichInQ--;
			break;
		case Monster.MonsterType.kraken:
			krakenInQ--;
			break;
		}
	}
}