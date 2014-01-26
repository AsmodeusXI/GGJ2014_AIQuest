using UnityEngine;
using System.Collections;

public class OnStartExecute : MonoBehaviour 
{
	private bool story1 = true;
	private bool story2 = false;
	private bool story3 = false;
	private bool spaceSkipping = false;
	private bool normalFading = false;

	void OnGUI()
	{
		Texture slide1 = (Texture)Resources.Load ("slide1");
		Texture slide2 = (Texture)Resources.Load ("slide2");
		Texture slide3 = (Texture)Resources.Load ("slide3");

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if(!normalFading)
			{
				spaceSkipping = true;
				AutoFade.LoadLevel("PlayScene",2,1,Color.black);
			}
		}

		//Start Coroutine to Wait for next story image
		Rect screenSize = new Rect (0, 0, Screen.width, Screen.height);

		//TODO REPLACE PH WITH STORY IMAGE
		if(story1)
		{
			GUI.DrawTexture( screenSize, slide1, ScaleMode.StretchToFill);
			Color thisColor = GUI.color;
			thisColor.a = 0.2f;
			GUI.color = thisColor;
			GUI.Label( new Rect(Screen.width/2/2/2/2,Screen.height-100,150,50), "Press Spacebar to skip");
			StartCoroutine (WaitAndChange (5, 1));
		}
		if(story2)
		{
			GUI.DrawTexture( screenSize, slide2, ScaleMode.StretchToFill);
			Color thisColor = GUI.color;
			thisColor.a = 0.2f;
			GUI.color = thisColor;
			GUI.Label( new Rect(Screen.width/2/2/2/2,Screen.height-100,150,50), "Press Spacebar to skip");
			StartCoroutine (WaitAndChange (5, 2));
		}
		if(story3)
		{
			GUI.DrawTexture( screenSize, slide3, ScaleMode.StretchToFill);
			Color thisColor = GUI.color;
			thisColor.a = 0.2f;
			GUI.color = thisColor;
			GUI.Label( new Rect(Screen.width/2/2/2/2,Screen.height-100,150,50), "Press Spacebar to skip");
			StartCoroutine (WaitAndChange (5, 3));
		}
	}

	IEnumerator WaitAndChange(int wait, int story)
	{
		yield return new WaitForSeconds(wait);
		if (story == 1 && !spaceSkipping) 
		{
			story1 = false;
			story2 = true;
		}
		if (story == 2 && !spaceSkipping) 
		{
			story2 = false;
			story3 = true;
		}
		if (story == 3 && !spaceSkipping) 
		{
			normalFading = true;
			AutoFade.LoadLevel ("PlayScene", 5, 1, Color.black);
		}

	}
}
