using UnityEngine;
using System.Collections;

public class AdversaryStats : MonoBehaviour {

	private float mood = 50;
	private int yourLvl= 1;
	private int kills  = 0;

	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private Vector3 	   healthScale;			// The local scale of the health bar initially (with full health).

	// Use this for initialization
	void Start () {
		
	}
 
	// Update is called once per frame
	void Update () {

		//mood can't go above 200
		if (mood > 100) {
			mood = 100;
		}
		//mood can't go below 0
		if (mood < 0) {
			mood = 0;
		}
		//if you get the guy's mood to 0 or 200 you lose
		if (mood == 0 || mood == 100) {
			goToGameOver ();
		}
		else{
			mood = (mood - (2f * (Time.deltaTime)));
		}
		
		Debug.Log ("Mood: " + mood + " Level: " + yourLvl);
		
		yourLvl = 1 + (kills/5);

	}
	//goes to new scene where the game could be restarted or quit out of
	public void goToGameOver(){
		
	}
	
	public float getMood() {
		return mood;
	}

	// Describes how a monster harms/improves the player when affecteds
	public void monsterAffects(float fun, float level) {
		mood = mood + fun * (level/yourLvl);
		kills++;
	}

}