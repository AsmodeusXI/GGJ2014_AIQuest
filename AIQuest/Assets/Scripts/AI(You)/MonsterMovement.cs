using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {


    public float percentPerSecond =.01f;
    public float endPercent = .75f;
    public float percentReductionEnd = .4f;

    private float percentComplete = 0;

    private Vector3 anchor;

    private Vector3 goal;
    private Transform me;
    
	// Use this for initialization
	void Start () {
        me = this.gameObject.transform;

        anchor = this.gameObject.transform.position;

        goal = GameObject.FindGameObjectWithTag("MonsterTarget").transform.position;
        
		Monster temp = gameObject.GetComponent<Monster>();
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		advStats.incrementMonsterQ(temp);
	}

	
	void OnDestroy() {
		Monster temp = gameObject.GetComponent<Monster>();
		GameObject adversaryObj = GameObject.FindGameObjectWithTag("Adversary");
		AdversaryStats advStats = (AdversaryStats) adversaryObj.GetComponent<AdversaryStats>();
		if(advStats != null)
		{
			advStats.decrementMonsterQ(temp);
			advStats.monsterAffects(temp);
		}
	}
	
    public void Update()
    {
        percentComplete += Time.deltaTime * percentPerSecond;
        
        
//        Debug.Log(string.Format("percentConvered: {0}: {1},{2}", percentComplete,this.gameObject.transform.position, /*anchor.transform.position,*/ goal ));


        me.transform.position = Vector3.Lerp(anchor, goal, percentComplete);
        float scaleMe = Mathf.Lerp(1, percentReductionEnd, percentComplete);
        me.transform.localScale = new Vector3(scaleMe, scaleMe, scaleMe);

        if(percentComplete >= endPercent)
        {
            Destroy(gameObject);
        }
    }
	
}