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
		Texture slide1 = (Texture)Resources.Load ("slide1");
		Texture slide2 = (Texture)Resources.Load ("slide2");
		Texture slide3 = (Texture)Resources.Load ("slide3");

		//Start Coroutine to Wait for next story image
		Rect screenSize = new Rect (0, 0, Screen.width, Screen.height);

		//TODO REPLACE PH WITH STORY IMAGE
		if(story1)
		{
			GUI.DrawTexture( screenSize, slide1, ScaleMode.StretchToFill);
			StartCoroutine (WaitAndChange (5, 1));
		}
		if(story2)
		{
			GUI.DrawTexture( screenSize, slide2, ScaleMode.StretchToFill);
			StartCoroutine (WaitAndChange (5, 2));
		}
		if(story3)
		{
			GUI.DrawTexture( screenSize, slide3, ScaleMode.StretchToFill);
			StartCoroutine (WaitAndChange (5, 3));
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
			AutoFade.LoadLevel ("PlayScene", 5, 1, Color.black);
		}

	}
}
