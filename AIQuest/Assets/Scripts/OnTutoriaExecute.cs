using UnityEngine;
using System.Collections;

public class OnTutoriaExecute : MonoBehaviour {

	void OnGUI()
	{
		Texture backgroundTex = (Texture)Resources.Load ("tutorialScreen");
		GUIStyle buttonStyle = new GUIStyle ();
		Texture backTex = (Texture)Resources.Load ("backbutton");

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTex);

		if (GUI.Button (new Rect (Screen.width * .04f, Screen.height * .85f, Screen.width * .2f, Screen.height * .2f), backTex, buttonStyle)) 
		{
			AutoFade.LoadLevel("OnGameLaunchScene",0,1,Color.black);
		}
	}
}
