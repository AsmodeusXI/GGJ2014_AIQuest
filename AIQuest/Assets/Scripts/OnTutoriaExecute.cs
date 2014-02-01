using UnityEngine;
using System.Collections;

public class OnTutoriaExecute : MonoBehaviour {

	void OnGUI()
	{
		Texture backgroundTex = (Texture)Resources.Load ("FullScreenImages/tutorialScreen");
		GUIStyle buttonStyle = new GUIStyle ();
		Texture backTex = (Texture)Resources.Load ("ButtonImages/backbutton");

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTex);

		if (GUI.Button (new Rect (Screen.width * .05f, Screen.height * .88f, Screen.width * .15f, Screen.height * .07f), backTex, buttonStyle)) 
		{
			AutoFade.LoadLevel("OnGameLaunchScene",0,1,Color.black);
		}
	}
}
