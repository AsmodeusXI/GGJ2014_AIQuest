using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {

	public float ySpeedModifier;
	public float zSpeedModifier;

	// Use this for initialization
	void Start () {
		//destroys after 3 seconds
		Destroy(gameObject, 1);
	}
	
	void OnDestroy() {
		Monster temp = gameObject.GetComponent<Monster>();
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		advStats.monsterAffects(temp);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//monsters move	
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, transform.localScale.y * ySpeedModifier, transform.localScale.z * zSpeedModifier);
	}
}