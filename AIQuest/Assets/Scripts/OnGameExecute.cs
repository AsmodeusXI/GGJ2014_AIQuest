using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class OnGameExecute : MonoBehaviour 
{
	private bool startEnabled = true;
	private bool creditsEnabled = true;
	private bool tutorialEnabled = true;

	void Start() {
		Social.localUser.Authenticate (ProcessAuthentication);
	}
	
	void ProcessAuthentication (bool success) {
		if (success) {
			Debug.Log ("Authenticated, checking achievements");
			
			// Request loaded achievements, and register a callback for processing them
			Social.LoadAchievements (ProcessLoadedAchievements);
		}
		else
			Debug.Log ("Failed to authenticate");
	}
	
	// This function gets called when the LoadAchievement call completes
	void ProcessLoadedAchievements (IAchievement[] achievements) {
		if (achievements.Length == 0)
			Debug.Log ("Error: no achievements found");
		else
			Debug.Log ("Got " + achievements.Length + " achievements");
		
		// You can also call into the functions like this
		Social.ReportProgress ("Achievement01", 100.0, result => {
			if (result)
				Debug.Log ("Successfully reported achievement progress");
			else
				Debug.Log ("Failed to report achievement");
		});
	}

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
		Texture gcTex = (Texture)Resources.Load ("ButtonImages/gcbutton");

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
		if (GUI.Button (new Rect (Screen.width * .05f, Screen.height * .5f, Screen.width * .2f, Screen.height * .13f), creditTex, buttonStyle)) 
		{
			startEnabled = false;
			creditsEnabled = false;
			tutorialEnabled =false;
			Application.LoadLevel("CreditScene");
		}

		//GUI enabled cause button to disable/enable
		GUI.enabled = tutorialEnabled;
		if (GUI.Button (new Rect (Screen.width * .05f, Screen.height * .65f,  Screen.width * .2f, Screen.height * .13f), tutorialTex, buttonStyle)) 
		{
			startEnabled = false;
			creditsEnabled = false;
			tutorialEnabled = false;
			Application.LoadLevel("TutorialScene");
		}

		//GUI enabled causes button to disable/enable
		if ((Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)) {
			if (GUI.Button (new Rect (Screen.width * .05f, Screen.height * .8f, Screen.width * .2f, Screen.height * .13f), gcTex, buttonStyle))  {
				Social.ShowLeaderboardUI ();
			}
		} else {
			if (GUI.Button (new Rect (Screen.width * .05f, Screen.height * .8f, Screen.width * .2f, Screen.height * .13f), exitTex, buttonStyle))  {
				Application.Quit();
			}
		}
	}
}
