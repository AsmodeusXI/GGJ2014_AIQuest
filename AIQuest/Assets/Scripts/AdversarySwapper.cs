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


	// Use this for initialization
	void Start () {
		
		SpriteRenderer spriter = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
		spriter.sprite = engagedSprite;
		
	}
	
	// Update is called once per frame
	void Update () {
	
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		float currentMood = advStats.getMood();
		
		SpriteRenderer spriter = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
		
		if (currentMood >= perturbedVal && currentMood < rageVal) {
			spriter.sprite = perturbedSprite;
		} else if (currentMood <= boredVal && currentMood > melancholyVal) {
			spriter.sprite = boredSprite;
		} else if (currentMood > boredVal && currentMood < perturbedVal) {
			spriter.sprite = engagedSprite;
		} else if (currentMood >= rageVal && currentMood < quitVal) {
			spriter.sprite = rageSprite;
		} else if (currentMood <= melancholyVal && currentMood > asleepVal) {
			spriter.sprite = melancholySprite;
		} else if (currentMood == quitVal) {
			spriter.sprite = quitSprite;
		} else if (currentMood == asleepVal) {
			spriter.sprite = asleepSprite;
		}

	}
}
