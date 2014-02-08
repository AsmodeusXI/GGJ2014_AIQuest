using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class Scoring : MonoBehaviour {

	public GameObject totalScore;
	public GameObject skeletonScore;
	public GameObject orcScore;
	public GameObject dragonScore;
	public GameObject lichScore;
	public GameObject timeScore;

	// Use this for initialization
	void Start () {
		setScoreText(totalScore, "Artful.Total.Spawns", "int");
		setScoreText(skeletonScore, "Artful.Skeleton.Spawns", "int");
		setScoreText(orcScore, "Artful.Orc.Spawns", "int");
		setScoreText(dragonScore, "Artful.Dragon.Spawns", "int");
		setScoreText(lichScore, "Artful.Lich.Spawns", "int");
		setScoreText(timeScore, "Artful.Zone.Time", "float");

		//Until Kraken
		ReportScore(PlayerPrefs.GetInt("Artful.Kraken.Spawns"), "Artful.Kraken.Spawns");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void setScoreText(GameObject scoreArea, string ppKey, string returnType) {
		GUIText scoreText = (GUIText)scoreArea.GetComponent<GUIText>();
		scoreText.fontSize = 40;
		if(returnType.Equals("int")) {
			scoreText.text = PlayerPrefs.GetInt(ppKey).ToString();
			ReportScore(PlayerPrefs.GetInt(ppKey), ppKey);
		} else if (returnType.Equals ("float")) {
			decimal decCast = (decimal) PlayerPrefs.GetFloat(ppKey);
			scoreText.text = decCast.ToString("n2");
			ReportScore(PlayerPrefs.GetInt(ppKey), ppKey);
		}
	}

	void ReportScore (long score, string leaderboardID) {
		Debug.Log ("Reporting score " + score + " on leaderboard " + leaderboardID);
		Social.ReportScore (score, leaderboardID, success => {
			Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});
	}
}
