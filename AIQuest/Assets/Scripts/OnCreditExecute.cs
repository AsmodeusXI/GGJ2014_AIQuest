using UnityEngine;
using System.Collections;

public class OnCreditExecute : MonoBehaviour
{
	void OnGUI()
	{
		Texture backgroundTex = (Texture)Resources.Load ("credits");
		Texture backTex = (Texture)Resources.Load ("backbutton");
		GUIStyle buttonStyle = new GUIStyle ();
		Rect sceneRect = new Rect (0, 0, Screen.width, Screen.height);

		GUI.DrawTexture (sceneRect, backgroundTex);

		//Creates a button to display "Back" button
		//TODO REPLACE BACK WITH BACK IMAGE
		if (GUI.Button(new Rect(Screen.width * .8f, Screen.height * .85f, Screen.width * .2f, Screen.height*.2f), backTex, buttonStyle)) 
		{
			AutoFade.LoadLevel("OnGameLaunchScene",0,.5f,Color.black);
		}
	}
}

