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

	private int spriteSwitch;
	private Sprite perturbedSprite;
	private Sprite zone1Sprite;
	private Sprite bored2Sprite;
	private Sprite zone3Sprite;
	private Sprite zone2Sprite;
	private Sprite euphoriaAlternateSprite;
	private Sprite bored1Sprite;
	private Sprite rage1Sprite;
	private Sprite rage2Sprite;
	private Sprite rageQuitSprite;
	private Sprite boredQuitSprite;
    public AdversarySoundPlayer adversaryPlayer;
	
	private bool timerOn = false;
	private float timeValue = 0;
    private bool playedSnore = false;
    private bool playedYell = false;


	// Use this for initialization
	void Start () {
		PlayerPrefs.SetFloat("Artful.Zone.Time", 0);
		setupImages();
		SpriteRenderer spriter = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
		spriter.sprite = zone1Sprite;
		
	}

	void setupImages() {
		spriteSwitch = PlayerPrefs.GetInt("Adversary");
		if (spriteSwitch == 0) {
			Debug.Log("John " + spriteSwitch);
//			perturbedSprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryPerturbed");
//			euphoriaAlternateSprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryEuphoria2");
			boredQuitSprite = Resources.Load<Sprite> ("AdversaryImages/John/quitBored");
			bored2Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryMellow");
			bored1Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryBored");
			zone1Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryEngaged");
			zone2Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryEnjoyment");
			zone3Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryEuphoria2");
			rage1Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryScared");
			rage2Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryRage");
			rageQuitSprite = Resources.Load<Sprite> ("AdversaryImages/John/quitRage");
		} else {
			Debug.Log("Jane" + spriteSwitch);
//			perturbedSprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Perturbed");
//			euphoriaAlternateSprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Euphoria");
			boredQuitSprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2BoredQuit");
			bored2Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Bored");
			bored1Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Mellow");
			zone1Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Enjoyment");
			zone2Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Enjoyment");
			zone3Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Euphoria");
			rage1Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Perturbed");
			rage2Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Rage");
			rageQuitSprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2RageQuit");

		}
		spriteSwitch = spriteSwitch > 0 ? 0 : 1;
		PlayerPrefs.SetInt ("Adversary", spriteSwitch);
		PlayerPrefs.Save ();
	}
	
	private void resetHappyTime() {
		timerOn = false;
		float currentHappyRecord = PlayerPrefs.GetFloat("Artful.Zone.Time");
		if (timeValue > currentHappyRecord) {
			PlayerPrefs.SetFloat("Artful.Zone.Time", timeValue);
		}
		timeValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		if (advStats.totalKills == 0) return;
		float currentMood = advStats.getRelativeMood();
		
		SpriteRenderer spriter = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
		
		if (currentMood >= perturbedVal && currentMood < rageVal) {
			spriter.sprite = rage1Sprite;
			resetHappyTime();
		} else if (currentMood <= boredVal && currentMood > melancholyVal) {
			spriter.sprite = bored2Sprite;
			resetHappyTime();
		} else if (currentMood > boredVal && currentMood < perturbedVal) {
			if(!timerOn) {
				spriter.sprite = zone1Sprite;
				timerOn = true;
			} else {
				timeValue += Time.deltaTime;
				if(timeValue > 7 && timeValue <= 18) {
					spriter.sprite = zone2Sprite;
				} else if (timeValue > 18) {
					spriter.sprite = zone3Sprite;
				}
			}
		} else if (currentMood >= rageVal && currentMood < quitVal) {
			spriter.sprite = rage2Sprite;
			resetHappyTime();
		} else if (currentMood <= melancholyVal && currentMood > asleepVal) {
			spriter.sprite = bored1Sprite;
			resetHappyTime();
		} else if (currentMood >= quitVal) {
			spriter.sprite = rageQuitSprite;
            if (!playedYell)
            {
                playedYell = true;
                adversaryPlayer.play(1);
            }
			resetHappyTime();
		} else if (currentMood <= asleepVal) {
			spriter.sprite = boredQuitSprite;
            if(!playedSnore)
            {
                playedSnore = true;
                adversaryPlayer.play(0);
            }
			resetHappyTime();
		}

	}
}
