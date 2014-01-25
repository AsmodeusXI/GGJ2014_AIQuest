using UnityEngine;
using System.Collections;

public class AdversaryStats : MonoBehaviour {

	private float mood = 50;
	private float lastMood = 50;
	private int yourLvl= 1;
	private int kills  = 0;

	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private Vector3 	   healthScale;			// The local scale of the health bar initially (with full health).

	// Use this for initialization
	void Start () {
		
	}
 
	// Update is called once per frame
	void Update () {
		//if you get the guy's mood to 0 or 200 you lose
		if (mood <= 0 || mood >= 100) {
			goToGameOver ();
		}
		lastMood = mood;
		mood = (mood - (2f * (Time.deltaTime)));
		
		yourLvl = 1 + (kills/5);
		checkMusic();
	}

	public void checkMusic() {
		Debug.Log ("Mood: " + mood);
		if (mood <35 && lastMood >= 35) {
			PlayClip("Music/FrusteratedPlayerState");
		} else if (mood > 65 && lastMood <= 65) {
			PlayClip("Music/BoredPlayerState");
		} else if (mood < 65 && lastMood >= 65) {
			PlayClip ("Music/NormalPlayerStateRevamped");
		} else if (mood > 30 && lastMood <= 30) {
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
	public void goToGameOver(){
		Application.LoadLevel("OnGameLaunchScene");
	}
	
	public float getMood() {
		return mood;
	}

	// Describes how a monster harms/improves the player when affecteds
	public void monsterAffects(float fun, float level) {
		lastMood = mood;
		//temporarily disabling effects of the affect.
		mood += 5;
		kills++;
		checkMusic();
	}

}