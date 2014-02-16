using UnityEngine;
using System.Collections;

public class OnCreditExecute : MonoBehaviour
{
	public Font buttonFont;
	void OnGUI()
	{
		Texture backgroundTex = (Texture)Resources.Load ("FullScreenImages/credits");
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.font = buttonFont;
		buttonStyle.fontSize = 30;
		Rect sceneRect = new Rect (0, 0, Screen.width, Screen.height);

		GUI.DrawTexture (sceneRect, backgroundTex);

		GUI.color = new Color(90f/255f, 220f/255f, 214f/255f, 1f);
		GUI.backgroundColor = new Color(33f/255f, 65f/255f, 156f/255f, 1f);
		if (GUI.Button(new Rect(Screen.width * .725f, Screen.height * .9f, Screen.width * .15f, Screen.height * .07f), "back", buttonStyle)) 
		{
			AutoFade.LoadLevel("OnGameLaunchScene",0,.5f,Color.black);
		}
	}
}

