using UnityEngine;
using System.Collections;

public class OnScoreExecute : MonoBehaviour {

	private bool exitEnabled = true;
	private bool replayEnabled = true;
	private bool menuEnabled = true;
	
	void OnGUI() {
	
		GUIStyle buttonStyle = new GUIStyle ();
		Texture gcTex = (Texture)Resources.Load ("ButtonImages/gcbutton");
		Texture exitTex = (Texture)Resources.Load ("ButtonImages/exitbutton");
		Texture replayTex = (Texture)Resources.Load ("ButtonImages/replay");
		Texture menuTex = (Texture)Resources.Load ("ButtonImages/menubutton");
		
		//GUI enabled causes button to disable/enable
		GUI.enabled = replayEnabled;
		if (GUI.Button (new Rect (Screen.width * .15f, Screen.height * .9f,Screen.width * .14f,Screen.height * .14f), replayTex, buttonStyle)) 
		{
			replayEnabled = false;
			menuEnabled = false;
			exitEnabled = false;
			AutoFade.LoadLevel("PlayScene",2,1,Color.black);
		}
		
		//GUI enabled causes button to disable/enable
		GUI.enabled = menuEnabled;

		if ((Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)) {
			if (GUI.Button (new Rect (Screen.width * .65f, Screen.height * .9f, Screen.width * .14f, Screen.height * .14f), menuTex, buttonStyle)) {
					replayEnabled = false;
					menuEnabled = false;
					exitEnabled = false;
					AutoFade.LoadLevel ("OnGameLaunchScene", 0, .5f, Color.black);
			}
		} else {
			if (GUI.Button (new Rect (Screen.width * .65f, Screen.height * .9f, Screen.width * .14f, Screen.height * .14f), menuTex, buttonStyle)) {	
				Application.Quit();
			}
		}
		
		//GUI enabled cause button to disable/enable
		GUI.enabled = exitEnabled;
		if ((Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)) {
			if (GUI.Button (new Rect (Screen.width * .39f, Screen.height * .9f, Screen.width * .14f, Screen.height * .14f), gcTex, buttonStyle)) {
					Social.ShowLeaderboardUI ();
			}
		} else {
			if (GUI.Button (new Rect (Screen.width * .39f, Screen.height * .9f, Screen.width * .14f, Screen.height * .14f), exitTex, buttonStyle)) {
				Application.Quit();
			}
		}
	}
}
