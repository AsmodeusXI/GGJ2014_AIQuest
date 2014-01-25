using UnityEngine;
using System.Collections;

public class OnStartExecute : MonoBehaviour 
{
	private bool story1 = true;
	private bool story2 = false;
	private bool story3 = false;
	private bool story4 = false;

	void OnGUI()
	{
		//Start Coroutine to Wait for next story image
		Rect screenSize = new Rect (0, 0, Screen.width, Screen.height);
		Rect screenSize1 = new Rect (0, 0, Screen.width, Screen.height);
		Rect screenSize2 = new Rect (0, 0, Screen.width, Screen.height);
		Rect screenSize3 = new Rect (0, 0, Screen.width, Screen.height);

		//TODO REPLACE PH WITH STORY IMAGE
		if(story1)
		{
			screenSize = GUI.Window (1, screenSize, Story, "PH");
			StartCoroutine (WaitAndChange (10, 1));
		}
		if(story2)
		{
			screenSize1 = GUI.Window (2, screenSize1, Story, "PH1");
			StartCoroutine (WaitAndChange (10, 2));
		}
		if(story3)
		{
			screenSize2 = GUI.Window (3, screenSize2, Story, "PH2");
			StartCoroutine (WaitAndChange (20, 3));
		}
		if(story4)
		{
			screenSize3 = GUI.Window (4, screenSize3, Story, "PH3");
			StartCoroutine (WaitAndChange (10, 4));
		}
	}

	IEnumerator WaitAndChange(int wait, int story)
	{
		yield return new WaitForSeconds(wait);
		if (story == 1) 
		{
			story1 = false;
			story2 = true;
		}
		if (story == 2) 
		{
			story2 = false;
			story3 = true;
		}
		if (story == 3) 
		{
			story3 = false;
			story4 = true;
		}
		if (story == 4) 
		{
			story4 = false;
			AutoFade.LoadLevel ("PlayScene", 5, 1, Color.black);
		}
	}

	private void Story(int windowID)
	{

	}
}
