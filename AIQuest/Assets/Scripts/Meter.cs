using UnityEngine;
using System.Collections;

public class Meter : MonoBehaviour {
	
	public float minPos; // -1.6019
	public float maxPos; // 2.37459
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Min Pos: " + minPos);
		Debug.Log ("Max Pos: " + maxPos);

	}
	
	// Update is called once per frame
	void Update () {
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		UpdateMeter(advStats.getMood());
	}
	
	// Takes a value 0-100.  This is translated into the mood bar.
	// 0 being the bottom and 10 being the top.
	void UpdateMeter(float mood){
		float ratio = mood / 100;
		
		Vector3 newPos = new Vector3 (transform.position.x, minPos + ((maxPos - minPos) * ratio), transform.position.z);
		Debug.Log ("delta: " + (maxPos - minPos).ToString());
		Debug.Log (newPos.ToString());
		transform.position = newPos;
		
	}
}
