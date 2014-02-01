using UnityEngine;
using System.Collections;

public class OnGameExecute : MonoBehaviour 
{
	private bool startEnabled = true;
	private bool creditsEnabled = true;
	private bool tutorialEnabled = true;

	void OnGUI()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Texture backgroundTex = (Texture)Resources.Load ("FullScreenImages/main_screen");
		GUIStyle buttonStyle = new GUIStyle ();
		buttonStyle.border = new RectOffset (0, 0, 0, 0);
		Texture startTex = (Texture)Resources.Load ("ButtonImages/startbutton");
		Texture creditTex = (Texture)Resources.Load ("ButtonImages/creditsbutton");
		Texture tutorialTex = (Texture)Resources.Load ("ButtonImages/tutorialbutton");
		Texture exitTex = (Texture)Resources.Load ("ButtonImages/exitbutton");

		Rect windowRect = new Rect(Screen.width/2-100, Screen.height/2-60, 200, 120);
		GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height), backgroundTex);

		//GUI enabled causes button to disable/enable
		GUI.enabled = startEnabled;
		if (GUI.Button (new Rect (Screen.width * .68f, Screen.height * .785f,Screen.width * .3f,Screen.height * .3f), startTex, buttonStyle)) 
		{
			startEnabled = false;
			creditsEnabled = false;
			tutorialEnabled = false;
			AutoFade.LoadLevel("StartScene",2,1,Color.black);
		}

		//GUI enabled causes button to disable/enable
		GUI.enabled = creditsEnabled;
		if (GUI.Button (new Rect (Screen.width * .06f, Screen.height * .65f, Screen.width * .15f, Screen.height * .07f), creditTex, buttonStyle)) 
		{
			startEnabled = false;
			creditsEnabled = false;
			tutorialEnabled =false;
			Application.LoadLevel("CreditScene");
		}

		//GUI enabled cause button to disable/enable
		GUI.enabled = tutorialEnabled;
		if (GUI.Button (new Rect (Screen.width * .06f, Screen.height * .75f, Screen.width * .15f, Screen.height * .07f), tutorialTex, buttonStyle)) 
		{
			startEnabled = false;
			creditsEnabled = false;
			tutorialEnabled = false;
			Application.LoadLevel("TutorialScene");
		}

		//GUI enabled causes button to disable/enable
		if (GUI.Button (new Rect (Screen.width * .06f, Screen.height * .85f, Screen.width * .15f, Screen.height * .07f), exitTex, buttonStyle)) 
		{
			Application.Quit();
		}
	}
}
