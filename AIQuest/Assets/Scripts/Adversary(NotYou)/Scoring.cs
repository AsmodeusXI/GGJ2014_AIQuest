using UnityEngine;
using System.Collections;

public class Scoring : MonoBehaviour {

	public GameObject totalScore;
	public GameObject skeletonScore;
	public GameObject orcScore;
	public GameObject dragonScore;
	public GameObject lichScore;
	public GameObject timeScore;

	// Use this for initialization
	void Start () {
		setScoreText(totalScore, "Total Spawns", "int");
		setScoreText(skeletonScore, "Skeleton Spawns", "int");
		setScoreText(orcScore, "Orc Spawns", "int");
		setScoreText(dragonScore, "Dragon Spawns", "int");
		setScoreText(lichScore, "Lich Spawns", "int");
		setScoreText(timeScore, "Happy Time", "float");
		StartCoroutine(Wait (6));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator Wait(int seconds) {
		yield return new WaitForSeconds(seconds);
		Application.LoadLevel("OnGameLaunchScene");
	}
	
	private void setScoreText(GameObject scoreArea, string ppKey, string returnType) {
		GUIText scoreText = (GUIText)scoreArea.GetComponent<GUIText>();
		scoreText.fontSize = 40;
		if(returnType.Equals("int")) {
			scoreText.text = PlayerPrefs.GetInt(ppKey).ToString();
		} else if (returnType.Equals ("float")) {
			decimal decCast = (decimal) PlayerPrefs.GetFloat(ppKey);
			scoreText.text = decCast.ToString("n2");
		}
	}
}
