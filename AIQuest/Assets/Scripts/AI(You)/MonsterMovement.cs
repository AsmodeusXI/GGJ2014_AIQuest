using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//destroys after 3 seconds
		Destroy(gameObject, 3);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//monsters move
		rigidbody2D.velocity = new Vector2(transform.localScale.x * 6, rigidbody2D.velocity.y);
	}
}