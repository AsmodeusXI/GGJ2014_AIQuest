using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {


    public float percentPerSecond;
    public float onscreenTime = 2f;
	private float totalTime = 0f;

    private float percentComplete = 0;

    private Vector3 anchor;

    private Vector3 goal;
    private Transform me;
	private Monster monster;
	private float startScale;
    
	// Use this for initialization
	void Start () {
		percentPerSecond = 1f/onscreenTime;
        me = this.gameObject.transform;
		monster = gameObject.GetComponent<Monster>();
		startScale = 1 + monster.charge;
		me.transform.localScale = new Vector3 (startScale, startScale, startScale);

        anchor = this.gameObject.transform.position;

        goal = GameObject.FindGameObjectWithTag("MonsterTarget").transform.position;
        
		Monster temp = gameObject.GetComponent<Monster>();
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		advStats.incrementMonsterQ(temp);
	}

	
	void OnDestroy() {
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		if(advStats != null)
		{
			advStats.decrementMonsterQ(monster);
			advStats.monsterAffects(monster);
		}
	}
	
    public void Update()
    {
        percentComplete += Time.deltaTime * percentPerSecond;
		totalTime += Time.deltaTime;

        me.transform.position = Vector3.Lerp(anchor, goal, percentComplete);
        float scaleMe = Mathf.Lerp(startScale, 0, percentComplete);
        me.transform.localScale = new Vector3(scaleMe, scaleMe, scaleMe);

        if(totalTime >= onscreenTime)
        {
			Debug.Log("P Complete:" + percentComplete);
            Destroy(gameObject);
        }
    }
	
}