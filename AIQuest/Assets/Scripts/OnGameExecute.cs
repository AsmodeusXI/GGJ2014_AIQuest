using UnityEngine;
using System.Collections;

public class OnGameExecute : MonoBehaviour 
{
	private bool startEnabled = true;
	private bool exitEnabled = true;
	private bool creditsEnabled = true;
	private bool tutorialEnabled = true;

	void OnGUI()
	{
		Texture backgroundTex = (Texture)Resources.Load ("main_screen");
		GUIStyle buttonStyle = new GUIStyle ();
		Texture startTex = (Texture)Resources.Load ("startbutton");
		Texture creditTex = (Texture)Resources.Load ("creditsbutton");
		Texture tutorialTex = (Texture)Resources.Load ("tutorialbutton");
		Texture exitTex = (Texture)Resources.Load ("exitbutton");

		Rect windowRect = new Rect(Screen.width/2-100, Screen.height/2-60, 200, 120);
		GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height), backgroundTex);

		//GUI enabled causes button to disable/enable
		GUI.enabled = startEnabled;
		if (GUI.Button (new Rect (Screen.width * .68f, Screen.height * .785f,Screen.width * .3f,Screen.height * .3f), startTex, buttonStyle)) 
		{
			startEnabled = false;
			creditsEnabled = false;
			exitEnabled = false;
			tutorialEnabled = false;
			AutoFade.LoadLevel("StartScene",2,1,Color.black);
		}

		//GUI enabled causes button to disable/enable
		GUI.enabled = creditsEnabled;
		if (GUI.Button (new Rect (Screen.width * .04f, Screen.height * .65f, Screen.width * .2f, Screen.height * .2f), creditTex, buttonStyle)) 
		{
			startEnabled = false;
			creditsEnabled = false;
			exitEnabled = false;
			tutorialEnabled =false;
			Application.LoadLevel("CreditScene");
		}

		//GUI enabled cause button to disable/enable
		GUI.enabled = tutorialEnabled;
		if (GUI.Button (new Rect (Screen.width * .04f, Screen.height * .75f, Screen.width * .2f, Screen.height * .2f), tutorialTex, buttonStyle)) 
		{
			startEnabled = false;
			creditsEnabled = false;
			exitEnabled = false;
			tutorialEnabled = false;
			Application.LoadLevel("TutorialScene");
		}

		//GUI enabled causes button to disable/enable
		GUI.enabled = exitEnabled;
		if (GUI.Button (new Rect (Screen.width * .04f, Screen.height * .85f, Screen.width * .2f, Screen.height * .2f), exitTex, buttonStyle)) 
		{
			Application.Quit();
		}
	}
}
