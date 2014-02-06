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
	private int skeletonsInQ;
	private int orcsInQ;
	private int dragonsInQ;
	private int lichInQ;
	private int krakenInQ;
	private bool gameOver;
	private float timeBetweenSpawns;
	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay;
	public float shake_intensity;
	public Transform camera;
	public bool monsterMode;
	public Monster.MonsterType monsterModeType;
	private float monsterModeTimer;


	void Start() {
		monsterMode = false;
		monsterModeType = Monster.MonsterType.skeleton;
		originPosition = camera.position;
		originRotation = camera.rotation;
		shake_decay = 0.002f;
	}
 
	// Update is called once per frame
	void Update () {
		if (gameOver) return;
		if (shake_intensity > 0){
			shakeScreen();
		}
		timeBetweenSpawns += Time.deltaTime;

		if (totalKills > 115) {
			checkMonsterMode();
		}
		
		if (mood <= 0 || mood >= (100 * yourLvl)) {
			gameOver = true;
			goToGameOver ();
		}
		mood = (mood - ((4f*(0.5f+ timeBetweenSpawns)) * (Time.deltaTime)));
		checkMusic();
	}

	void checkMonsterMode() {
		if (monsterModeTimer <= 0 && !monsterMode) {
			monsterModeTimer = UnityEngine.Random.Range (10 / yourLvl, 30 / yourLvl);
			monsterMode = true;
			float rannum = UnityEngine.Random.Range(0f, 1f) * 4;
			monsterModeType = (Monster.MonsterType)(System.Math.Round(rannum));
//			playClipForMonsterType(monsterModeType);
		} else {
			monsterModeTimer -= Time.deltaTime;
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

	void shake(int intensity){
		shake_intensity += intensity * 0.002f;
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
		PlayerPrefs.SetInt("Total Spawns", totalKills);
		PlayerPrefs.SetInt("Skeleton Spawns", skeletonKills);
		PlayerPrefs.SetInt("Orc Spawns", orcKills);
		PlayerPrefs.SetInt("Dragon Spawns", dragonKills);
		PlayerPrefs.SetInt("Lich Spawns", lichKills);
		PlayerPrefs.SetInt("Kraken Spawns", krakenKills);
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
		if (monsterMode) {
			if (monster.type == monsterModeType) {
				monsterMode = false;
				return;
			}
		}

		timeBetweenSpawns = 0;
		checkMusic();
		if (tooLowLevel (monster)) return;	
		shake ((((int)monster.type + 1) * monster.getLevel()) * 3);
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
		switch (monster.type) {
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