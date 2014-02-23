using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class Scoring : MonoBehaviour {

	public GameObject totalScore;
	public GameObject skeletonScore;
	public GameObject orcScore;
	public GameObject dragonScore;
	public GameObject lichScore;
	public GameObject krakenScore;
	public GameObject timeScore;
	public GameObject powerScore;

	// Use this for initialization
	void Start () {
		setScoreText(totalScore, "Artful.Total.Spawns", "int");
		setScoreText(skeletonScore, "Artful.Skeleton.Spawns", "int");
		setScoreText(orcScore, "Artful.Orc.Spawns", "int");
		setScoreText(dragonScore, "Artful.Dragon.Spawns", "int");
		setScoreText(lichScore, "Artful.Lich.Spawns", "int");
		setScoreText(timeScore, "Artful.Zone.Time", "float");
		setScoreText(krakenScore, "Artful.Kraken.Spawns", "int");
		setScoreText(powerScore, "Artful.Max.Intensity", "int");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void setScoreText(GameObject scoreArea, string ppKey, string returnType) {
		GUIText scoreText = (GUIText)scoreArea.GetComponent<GUIText>();
		if(returnType.Equals("int")) {
			bool isMax = checkMaxScore(PlayerPrefs.GetInt(ppKey), ppKey);
			scoreText.text = PlayerPrefs.GetInt(ppKey).ToString() + (isMax ? "*" : "");
		} else if (returnType.Equals ("float")) {
			bool isMax = checkMaxScore(Mathf.FloorToInt(PlayerPrefs.GetFloat(ppKey)), ppKey);
			decimal decCast = (decimal) PlayerPrefs.GetFloat(ppKey);
			scoreText.text = decCast.ToString("n1") + (isMax ? "*" : "");
		}
	}

	bool checkMaxScore(int currentScore, string ppKey) {
		int maxScore = PlayerPrefs.GetInt(ppKey + ".Max");
		if (currentScore > maxScore) {
			PlayerPrefs.SetInt(ppKey + ".Max", currentScore);
			ReportScore(currentScore, ppKey);
			return true;
		} else {
			int retry = PlayerPrefs.GetInt("Retry." + ppKey);
			if (retry > 0) {
				ReportScore(maxScore, ppKey);
			}
			return false;
		}
	}

	void ReportScore (long score, string leaderboardID) {
		Debug.Log ("Reporting score " + score + " on leaderboard " + leaderboardID);
		Social.ReportScore (score, leaderboardID, success => {
			if (success) {
				PlayerPrefs.SetInt("Retry." + leaderboardID, 0);
			} else {
				PlayerPrefs.SetInt("Retry." + leaderboardID, 1);
			}
		});
	}
}
