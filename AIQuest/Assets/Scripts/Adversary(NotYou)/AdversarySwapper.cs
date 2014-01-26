using UnityEngine;
using System.Collections;

public class AdversarySwapper : MonoBehaviour {

	private int quitVal = 100;
	private int rageVal = 85;
	private int perturbedVal = 65;
	//private int euphoriaVal = 55;
	private int boredVal = 35;
	private int melancholyVal = 15;
	private int asleepVal = 0;
	
	public Sprite perturbedSprite;
	public Sprite engagedSprite;
	public Sprite boredSprite;
	public Sprite enjoyingSprite;
	public Sprite euphoriaSprite;
	public Sprite melancholySprite;
	public Sprite rageSprite;
	public Sprite quitSprite;
	public Sprite asleepSprite;
	
	private bool timerOn = false;
	private float timeValue = 0;


	// Use this for initialization
	void Start () {
		
		SpriteRenderer spriter = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
		spriter.sprite = engagedSprite;
		PlayerPrefs.SetFloat("Happy Time", timeValue);
		
	}
	
	private void resetHappyTime() {
		timerOn = false;
		float currentHappyRecord = PlayerPrefs.GetFloat("Happy Time");
		if (timeValue > currentHappyRecord) {
			PlayerPrefs.SetFloat("Happy Time", timeValue);
		}
		timeValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		float currentMood = advStats.getRelativeMood();
		
		SpriteRenderer spriter = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
		
		if (currentMood >= perturbedVal && currentMood < rageVal) {
			spriter.sprite = perturbedSprite;
			resetHappyTime();
		} else if (currentMood <= boredVal && currentMood > melancholyVal) {
			spriter.sprite = boredSprite;
			resetHappyTime();
		} else if (currentMood > boredVal && currentMood < perturbedVal) {
			if(!timerOn) {
				spriter.sprite = engagedSprite;
				timerOn = true;
			} else {
				timeValue += Time.deltaTime;
				if(timeValue > 7) {
					spriter.sprite = enjoyingSprite;
				} else if (timeValue > 18) {
					spriter.sprite = euphoriaSprite;
				}
			}
		} else if (currentMood >= rageVal && currentMood < quitVal) {
			spriter.sprite = rageSprite;
			resetHappyTime();
		} else if (currentMood <= melancholyVal && currentMood > asleepVal) {
			spriter.sprite = melancholySprite;
			resetHappyTime();
		} else if (currentMood >= quitVal) {
			spriter.sprite = quitSprite;
			resetHappyTime();
		} else if (currentMood <= asleepVal) {
			spriter.sprite = asleepSprite;
			resetHappyTime();
		}

	}
}
