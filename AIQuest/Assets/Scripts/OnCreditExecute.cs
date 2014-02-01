using UnityEngine;
using System.Collections;

public class OnCreditExecute : MonoBehaviour
{
	void OnGUI()
	{
		Texture backgroundTex = (Texture)Resources.Load ("FullScreenImages/credits");
		Texture backTex = (Texture)Resources.Load ("ButtonImages/backbutton");
		GUIStyle buttonStyle = new GUIStyle ();
		Rect sceneRect = new Rect (0, 0, Screen.width, Screen.height);

		GUI.DrawTexture (sceneRect, backgroundTex);

		//Creates a button to display "Back" button
		//TODO REPLACE BACK WITH BACK IMAGE
		if (GUI.Button(new Rect(Screen.width * .725f, Screen.height * .9f, Screen.width * .15f, Screen.height * .07f), backTex, buttonStyle)) 
		{
			AutoFade.LoadLevel("OnGameLaunchScene",0,.5f,Color.black);
		}
	}
}

