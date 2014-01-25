using UnityEngine;
using System.Collections;

public class OnCreditExecute : MonoBehaviour
{
	void OnGUI()
	{
		Rect sceneRect = new Rect (0, 0, Screen.width, Screen.height);
		//TODO REPLACE PH WITH CREDITS ART
		GUI.Box (sceneRect, "PH");

		//Creates a button to display "Back" button
		//TODO REPLACE BACK WITH BACK IMAGE
		if (GUI.Button(new Rect(sceneRect.width-100,sceneRect.height-60,70,30), "Back")) 
		{
			Application.LoadLevel("OnGameLaunchScene");
		}
	}
}

