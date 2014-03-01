using UnityEngine;
using System.Collections;

public class BossApproach : MonoBehaviour {

	public GameObject advObject;
	public AdversaryStats advStats;
	public GUIText bossApproachText;
	private Color textColor;
	private float TimeToSwitch;
	public int BossLevel = 250;

	// Use this for initialization
	void Start () {
		GameObject advObject = GameObject.FindGameObjectWithTag("Adversary");
		advStats = (AdversaryStats) advObject.GetComponent<AdversaryStats>();

		bossApproachText = (GUIText)this.gameObject.GetComponent<GUIText>();
		textColor = bossApproachText.color;
		bossApproachText.color = Color.clear;

	}
	
	// Update is called once per frame
	void Update () {
		if (advStats.totalKills > BossLevel - 10 && !advStats.bossInQ) {
			flashColor();
		} else {
			bossApproachText.color = Color.clear;
		}
	
	}

	void flashColor() {
		TimeToSwitch -= Time.deltaTime;
		if (TimeToSwitch <= 0) {
			bossApproachText.color = bossApproachText.color == textColor ? Color.red : textColor;
			float totalKillsFloat = advStats.totalKills;
			float bossLevelFloat = BossLevel;
			TimeToSwitch = Mathf.Max((1f - totalKillsFloat/bossLevelFloat), 0.05f);
		} 
	}
}
