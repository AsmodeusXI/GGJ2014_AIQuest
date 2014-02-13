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
		Texture slide1 = (Texture)Resources.Load ("FullScreenImages/slide1");
		Texture slide2 = (Texture)Resources.Load ("FullScreenImages/slide2");
		Texture slide3 = (Texture)Resources.Load ("FullScreenImages/slide3");

		if (Input.GetKeyDown (KeyCode.Space) || Input.touches.Length > 0) 
		{
			if(!normalFading)
			{
				spaceSkipping = true;
				AutoFade.LoadLevel("PlayScene",2,1,Color.black);
			}
		}

		//Start Coroutine to Wait for next story image
		Rect screenSize = new Rect (0, 0, Screen.width, Screen.height);

		string text = "Press Spacebar to skip";

		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			text = "Tap Screen to skip";
		}

		if(story1)
		{
			GUI.DrawTexture( screenSize, slide1, ScaleMode.StretchToFill);
			Color thisColor = Color.white;
			thisColor.a = 1f;
			GUI.color = thisColor;
			GUI.Label( new Rect(45,Screen.height-50,250,120), text);
			StartCoroutine (WaitAndChange (6, 1));
		}
		if(story2)
		{
			GUI.DrawTexture( screenSize, slide2, ScaleMode.StretchToFill);
			Color thisColor = Color.white;
			thisColor.a = 1f;
			GUI.color = thisColor;
			GUI.Label( new Rect(45,Screen.height-50,250,120), text);
			StartCoroutine (WaitAndChange (6, 2));
		}
		if(story3)
		{
			GUI.DrawTexture( screenSize, slide3, ScaleMode.StretchToFill);
//			Color thisColor = Color.white;
//			thisColor.a = 0.5f;
//			GUI.color = thisColor;
//			GUI.Label( new Rect(45,Screen.height-50,250,120), text);
			StartCoroutine (WaitAndChange (7, 3));
			if (audio.volume > 0) {
				audio.volume -= Time.deltaTime/75;
			}
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
