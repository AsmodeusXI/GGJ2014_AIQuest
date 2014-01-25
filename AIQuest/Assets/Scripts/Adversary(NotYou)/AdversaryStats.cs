using UnityEngine;
using System.Collections;

public class AdversaryStats : MonoBehaviour {

	public float health = 100;
	public float mood   = 100;
	public float yourLvl= 1;
	public float kills  = 0;

	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private Vector3 	   healthScale;			// The local scale of the health bar initially (with full health).

	// Use this for initialization
	void Start () {
		
	}
 
	// Update is called once per frame
	void Update () {
		//health can't go above 100
		if (health > 100) {
			health = 100;
		}
		//health can't go below 0
		if (health < 0) { 
			health = 0;
		}
		//mood can't go above 200
		if (mood > 200) {
			mood = 200;
		}
		//mood can't go below 0
		if (mood < 0) {
			mood = 0;
		}
		//if you get the guy's mood to 0 or 200 you lose
		if (mood == 0 || mood == 200) {
			goToGameOver ();
		}
		else{
			mood = (mood - (2f * (Time.deltaTime)));
		}
		
		Debug.Log ("Mood: " + mood);
		
		yourLvl = 1 + (kills/5);
	}
	//goes to new scene where the game could be restarted or quit out of
	public void goToGameOver(){
		
	}

	// Describes how a monster harms/improves the player when affecteds
	public void monsterAffects(float damage, float fun, float level){
		health = health - damage * (yourLvl/level);
		mood   = mood + fun * (level/yourLvl);
		kills++;
	}

	public void UpdateHealthBar (){
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 100 - health * 0.01f);
		
		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}
}