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
		Rect windowRect = new Rect(Screen.width/2-100, Screen.height/2-60, 200, 120);
		//GUI enabled causes button to disable/enable
		GUI.enabled = startEnabled;
		if (GUI.Button (new Rect (Screen.width/2-40, Screen.height/2-30, 80, 20), "Start")) 
		{
			//Load new scene here for game to commence
		}

		GUI.enabled = creditsEnabled;
		if (GUI.Button (new Rect (Screen.width / 2 - 40, Screen.height / 2, 80, 20), "Credits")) 
		{

		}

		//GUI enabled causes button to disable/enable
		GUI.enabled = exitEnabled;
		if (GUI.Button (new Rect (Screen.width/2-40, Screen.height/2+30, 80, 20), "Quit")) 
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
		if (GUILayout.Button("Confirm Exit") )
		{
			Application.Quit();
		}

		//Cancels confirm window and re-enables main buttons
		if (GUILayout.Button("Cancel Exit") )
		{
			ExitGame = false;
			startEnabled = true;
			exitEnabled = true;
			creditsEnabled = true;
		}
	}
}
