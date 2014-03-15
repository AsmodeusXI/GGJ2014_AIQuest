﻿using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {


    public float percentPerSecond;
    public float onscreenTime = 2f;
	private float totalTime = 0f;

    private float percentComplete = 0;

    private Vector3 anchor;

    private Vector3 goal;
	private Vector3 tempGoal;
    private Transform me;
	private Monster monster;
	private float startScale;
	private float endScale = 0;
    
	// Use this for initialization
	void Start () {
        me = this.gameObject.transform;
		monster = gameObject.GetComponent<Monster>();
		if (monster.type == Monster.MonsterType.boss) {
			onscreenTime = 4f;
			endScale = 1;
		} else {
			startScale = 1 + monster.charge;
			me.transform.localScale = new Vector3 (startScale, startScale, startScale);
		}
		percentPerSecond = 1f/onscreenTime;

        anchor = this.gameObject.transform.position;

        goal = GameObject.FindGameObjectWithTag("MonsterTarget").transform.position;

		tempGoal = goal;
		tempGoal.y = goal.y + Random.Range (-(anchor.y / 2), (anchor.y / 2));
		tempGoal.x = goal.x + Random.Range (-(anchor.x / 2), (anchor.x / 2));
		tempGoal.z = goal.z + Random.Range (-(anchor.z / 2), (anchor.z / 2));
        
		Monster temp = gameObject.GetComponent<Monster>();
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		advStats.incrementMonsterQ(temp);
	}

	
	void OnDestroy() {

	}
	
    public void Update()
    {
        percentComplete += Time.deltaTime * percentPerSecond;
		totalTime += Time.deltaTime;

        me.transform.position = Vector3.Lerp(anchor, tempGoal, percentComplete);

		if (monster.type != Monster.MonsterType.kraken) { 
	        float scaleMe = Mathf.Lerp(startScale, endScale, percentComplete);
	        me.transform.localScale = new Vector3(scaleMe, scaleMe, scaleMe);
		}
        if(totalTime >= onscreenTime)
        {
			GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
			AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
			if(advStats != null)
			{
				advStats.decrementMonsterQ(monster);
				advStats.monsterAffects(monster);
			}
            Destroy(gameObject);
        }
    }
	
}