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

	public int spriteSwitch;
	public int player = 0;
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
	private Sprite winSprite;
    public AdversarySoundPlayer adversaryPlayer;
	
	private bool timerOn = false;
	private float timeValue = 0;
    private bool playedSnore = false;
    private bool playedYell = false;
	private AdversaryStats advStats;

	private int numberOfPlayers;


	// Use this for initialization
	void Start () {
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
		if (player > numberOfPlayers) {
			Destroy(gameObject);
			return;
		} 
		PlayerPrefs.SetFloat("Artful.Zone.Time", 0);
		setupImages();
		SpriteRenderer spriter = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
		spriter.sprite = zone1Sprite;
		
	}

	void setupImages() {
		spriteSwitch = PlayerPrefs.GetInt("Adversary");
		if (spriteSwitch == 0) {
			boredQuitSprite = Resources.Load<Sprite> ("AdversaryImages/John/quitBored");
			bored2Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryMellow");
			bored1Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryBored");
			zone1Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryEngaged");
			zone2Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryEnjoyment");
			zone3Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryEuphoria2");
			rage1Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryScared");
			rage2Sprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryRage");
			rageQuitSprite = Resources.Load<Sprite> ("AdversaryImages/John/quitRage");
			winSprite = Resources.Load<Sprite> ("AdversaryImages/John/adversaryWin");
		} else {
			boredQuitSprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2BoredQuit");
			bored2Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Bored");
			bored1Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Mellow");
			zone1Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Enjoyment");
			zone2Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Enjoyment");
			zone3Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Euphoria");
			rage1Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Perturbed");
			rage2Sprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Rage");
			rageQuitSprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2RageQuit");
			winSprite = Resources.Load<Sprite> ("AdversaryImages/Jane/adversary2Win");
		}
		spriteSwitch = spriteSwitch > 0 ? 0 : 1;
		PlayerPrefs.SetInt ("Adversary", spriteSwitch);
		PlayerPrefs.Save ();
	}
	
	private void resetHappyTime() {
		timerOn = false;
		string hardScore = numberOfPlayers == 0 ? "" : ".Hard";
		float currentHappyRecord = PlayerPrefs.GetFloat("Artful.Zone.Time" + hardScore);
		if (timeValue > currentHappyRecord) {
			PlayerPrefs.SetFloat("Artful.Zone.Time" + hardScore, timeValue);
		}
		timeValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (advStats.totalKills == 0) return;
		float currentMood = advStats.getRelativeMood(player);
		
		SpriteRenderer spriter = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();

		if (advStats.bossDefeated) {
			spriter.sprite = winSprite;
			return;
		}

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
				if (spriteSwitch == 1) {
                adversaryPlayer.play(1);
				} else {
				adversaryPlayer.play(3);
				}
            }
			resetHappyTime();
		} else if (currentMood <= asleepVal) {
			spriter.sprite = boredQuitSprite;
            if(!playedSnore)
            {
                playedSnore = true;
				if (spriteSwitch == 1) {
					adversaryPlayer.play(0);
				} else {
					adversaryPlayer.play(2);
				}
            }
			resetHappyTime();
		}

	}
}
