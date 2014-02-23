using UnityEngine;
using System.Collections;
using System;

public class Meter : MonoBehaviour {
	
	public float minPos; // -1.6019
	public float maxPos; // 2.37459
	public GameObject meter1;
	public GameObject meter2;
	private AdversaryStats advStats;
	
	// Use this for initialization
	void Start () {
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		int numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
		if (numberOfPlayers == 0) {
			meter2.renderer.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMeter(0);
		UpdateMeter(1);
	}
	
	// Takes a value 0-100.  This is translated into the mood bar.
	// 0 being the bottom and 10 being the top.
	void UpdateMeter(int currentPlayer){
		float mood = advStats.getRelativeMood (currentPlayer);
		mood = Math.Min(Math.Max(0, mood), 100);
		float ratio = mood / 100;
		if (currentPlayer == 0) {
			Vector3 newPos = new Vector3 (meter1.transform.position.x, minPos + ((maxPos - minPos) * ratio), meter1.transform.position.z);
			meter1.transform.position = newPos;
		} else {
			Vector3 newPos = new Vector3 (meter2.transform.position.x, minPos + ((maxPos - minPos) * ratio), meter2.transform.position.z);
			meter2.transform.position = newPos;
		}
		
	}
}
