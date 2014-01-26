using UnityEngine;
using System.Collections;

public class OnGameExecute : MonoBehaviour 
{
	private bool ExitGame = false;
	private bool startEnabled = true;
	private bool exitEnabled = true;
	private bool creditsEnabled = true;

	void OnGUI()
	{
		GUIStyle buttonStyle = new GUIStyle ();
		Texture startTex = (Texture)Resources.Load ("orcbutton");
		Texture creditTex = (Texture)Resources.Load ("lichbutton");
		Texture exitTex = (Texture)Resources.Load ("dragonbutton");

		Rect windowRect = new Rect(Screen.width/2-100, Screen.height/2-60, 200, 120);
		//GUI enabled causes button to disable/enable
		GUI.enabled = startEnabled;
		if (GUI.Button (new Rect (Screen.width/2-40, Screen.height/2-75, 150, 50), startTex, buttonStyle)) 
		{
			startEnabled = false;
			creditsEnabled = false;
			exitEnabled = false;
			AutoFade.LoadLevel("StartScene",3,1,Color.black);
		}

		//GUI enabled causes button to disable/enable
		GUI.enabled = creditsEnabled;
		if (GUI.Button (new Rect (Screen.width / 2 - 40, Screen.height / 2, 150, 50), creditTex, buttonStyle)) 
		{
			startEnabled = false;
			creditsEnabled = false;
			exitEnabled = false;
			Application.LoadLevel("CreditScene");
		}

		//GUI enabled causes button to disable/enable
		GUI.enabled = exitEnabled;
		if (GUI.Button (new Rect (Screen.width/2-40, Screen.height/2+75, 150, 50), exitTex, buttonStyle)) 
		{
			//ExitGame to true for Window Pop-up
			ExitGame = true;
			//Disables Exit and Start buttons
			startEnabled = false;
			exitEnabled = false;
			creditsEnabled = false;
		}

		//Sets GUI enabled to true just in case
		GUI.enabled = true;
		if(ExitGame)
			windowRect = GUI.Window(0, windowRect, ExitConfirm, "PH");
	}

	private void ExitConfirm(int windowID)
	{
		//Closes application
		if (GUI.Button(new Rect(10,35,180,25), "Confirm Exit") )
		{
			Application.Quit();
		}

		//Cancels confirm window and re-enables main buttons
		if (GUI.Button(new Rect(10,75,180,25), "Cancel Exit") )
		{
			ExitGame = false;
			startEnabled = true;
			exitEnabled = true;
			creditsEnabled = true;
		}
	}
}
