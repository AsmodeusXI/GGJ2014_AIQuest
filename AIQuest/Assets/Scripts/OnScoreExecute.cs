using UnityEngine;
using System.Collections;

public class OnScoreExecute : MonoBehaviour {

	private bool exitEnabled = true;
	private bool replayEnabled = true;
	private bool menuEnabled = true;
	private ADBannerView banner = null;
	public Font buttonFont;

	void Start () {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			banner = new ADBannerView(ADBannerView.Type.Banner, ADBannerView.Layout.Top);
			ADBannerView.onBannerWasClicked += OnBannerClicked;
			ADBannerView.onBannerWasLoaded  += OnBannerLoaded;
		}
	}

	void OnDestroy() {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
						ADBannerView.onBannerWasClicked -= OnBannerClicked;
						ADBannerView.onBannerWasLoaded -= OnBannerLoaded;
						banner.visible = false;
				}
	}
	
	void OnGUI() {

		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.font = buttonFont;
		buttonStyle.fontSize = 30;
		GUI.color = new Color(90f/255f, 220f/255f, 214f/255f, 1f);
		GUI.backgroundColor = new Color(33f/255f, 65f/255f, 156f/255f, 1f);
		
		//GUI enabled causes button to disable/enable
		GUI.enabled = replayEnabled;
		if (GUI.Button (new Rect (Screen.width * .02f, Screen.height * .9f,Screen.width * .22f,Screen.height * .075f), "replay", buttonStyle)) 
		{
			replayEnabled = false;
			menuEnabled = false;
			exitEnabled = false;
			if (Application.platform == RuntimePlatform.IPhonePlayer) {banner.visible = false;}
			AutoFade.LoadLevel("PlayScene",2,1,Color.black);
		}
		
		//GUI enabled causes button to disable/enable
		GUI.enabled = menuEnabled;

		if (GUI.Button (new Rect (Screen.width * .75f, Screen.height * .9f, Screen.width * .22f, Screen.height * .075f), "menu", buttonStyle)) {
				replayEnabled = false;
				menuEnabled = false;
				exitEnabled = false;
				if (Application.platform == RuntimePlatform.IPhonePlayer) {banner.visible = false;}
				AutoFade.LoadLevel ("OnGameLaunchScene", 0, .5f, Color.black);
		}
		
		//GUI enabled cause button to disable/enable
		GUI.enabled = exitEnabled;
		if ((Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)) {
			if (GUI.Button (new Rect (Screen.width * .39f, Screen.height * .9f, Screen.width * .22f, Screen.height * .075f), "scores", buttonStyle)) {
					Social.ShowLeaderboardUI ();
			}
		} else {
			if (GUI.Button (new Rect (Screen.width * .39f, Screen.height * .9f, Screen.width * .22f, Screen.height * .075f), "exit", buttonStyle)) {
				Application.Quit();
			}
		}
	}

	void OnBannerClicked()
	{
		Debug.Log("Clicked!\n");
	}
	void OnBannerLoaded()
	{
		Debug.Log("Loaded!\n");
		banner.visible = true;
	}
}
