using UnityEngine;
using System.Collections;
using System;

public class AdversaryStats : MonoBehaviour {

	private float mood = 50;
	private int yourLvl= 1;
	private int totalKills  = 0;
	private int skeletonKills = 0;
	private int orcKills = 0;
	private int lichKills = 0;
	private int dragonKills = 0;
	private bool gameOver;
	private float timeBetweenSpawns;
 
	// Update is called once per frame
	void Update () {
		if (gameOver) return;
		timeBetweenSpawns += Time.deltaTime;

//		Debug.Log ("mood: " + mood + " relative: " + getRelativeMood() + " max: " + (100 * yourLvl));
		if (mood <= 0 || mood >= (100 * yourLvl)) {
			gameOver = true;
			goToGameOver ();
		}
		mood = (mood - ((4f*(0.5f+ timeBetweenSpawns)) * (Time.deltaTime)));
		checkMusic();
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
		timeBetweenSpawns = 0;
		checkMusic();
		if (tooLowLevel (monster)) return;
		mood += (((int)monster.type + 1) * monster.getLevel()) * 3;
		incrementMonsterKills (monster);
		totalKills++;
		yourLvl = Math.Max (1, totalKills / 50);
		Debug.Log ("total spawned: " + totalKills);
	}

	public bool tooLowLevel(Monster monster) {
		return (monsterKills(monster) > monster.getLevel() * 10);
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
		}		
	}
}