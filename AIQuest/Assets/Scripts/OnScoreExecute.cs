using UnityEngine;
using System.Collections;

public class OnScoreExecute : MonoBehaviour {

	private bool exitEnabled = true;
	private bool replayEnabled = true;
	private bool menuEnabled = true;
	
	void OnGUI() {
	
		GUIStyle buttonStyle = new GUIStyle ();
		Texture exitTex = (Texture)Resources.Load ("exitbutton");
		Texture replayTex = (Texture)Resources.Load ("replay");
		Texture menuTex = (Texture)Resources.Load ("menubutton");
		
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
		if (GUI.Button (new Rect (Screen.width * .65f, Screen.height * .9f, Screen.width * .14f, Screen.height * .14f), menuTex, buttonStyle)) 
		{
			replayEnabled = false;
			menuEnabled = false;
			exitEnabled = false;
			AutoFade.LoadLevel("OnGameLaunchScene",0,.5f,Color.black);
		}
		
		//GUI enabled cause button to disable/enable
		GUI.enabled = exitEnabled;
		if (GUI.Button (new Rect (Screen.width * .39f, Screen.height * .9f, Screen.width * .14f, Screen.height * .14f), exitTex, buttonStyle)) 
		{
			Application.Quit ();
		}
	
	}
}
