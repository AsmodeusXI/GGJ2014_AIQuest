using UnityEngine;
using System.Collections;

public class OnScoreExecute : MonoBehaviour {

	private bool exitEnabled = true;
	private bool replayEnabled = true;
	private bool menuEnabled = true;
	private ADBannerView banner = null;

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

		GUIStyle buttonStyle = new GUIStyle ();
		Texture gcTex = (Texture)Resources.Load ("ButtonImages/gcbutton");
		Texture exitTex = (Texture)Resources.Load ("ButtonImages/exitbutton");
		Texture replayTex = (Texture)Resources.Load ("ButtonImages/replay");
		Texture menuTex = (Texture)Resources.Load ("ButtonImages/menubutton");
		
		//GUI enabled causes button to disable/enable
		GUI.enabled = replayEnabled;
		if (GUI.Button (new Rect (Screen.width * .15f, Screen.height * .9f,Screen.width * .14f,Screen.height * .14f), replayTex, buttonStyle)) 
		{
			replayEnabled = false;
			menuEnabled = false;
			exitEnabled = false;
			if (Application.platform == RuntimePlatform.IPhonePlayer) {banner.visible = false;}
			AutoFade.LoadLevel("PlayScene",2,1,Color.black);
		}
		
		//GUI enabled causes button to disable/enable
		GUI.enabled = menuEnabled;

		if (GUI.Button (new Rect (Screen.width * .65f, Screen.height * .9f, Screen.width * .14f, Screen.height * .14f), menuTex, buttonStyle)) {
				replayEnabled = false;
				menuEnabled = false;
				exitEnabled = false;
				if (Application.platform == RuntimePlatform.IPhonePlayer) {banner.visible = false;}
				AutoFade.LoadLevel ("OnGameLaunchScene", 0, .5f, Color.black);
		}
		
		//GUI enabled cause button to disable/enable
		GUI.enabled = exitEnabled;
		if ((Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)) {
			if (GUI.Button (new Rect (Screen.width * .39f, Screen.height * .9f, Screen.width * .14f, Screen.height * .14f), gcTex, buttonStyle)) {
					Social.ShowLeaderboardUI ();
			}
		} else {
			if (GUI.Button (new Rect (Screen.width * .39f, Screen.height * .9f, Screen.width * .14f, Screen.height * .14f), exitTex, buttonStyle)) {
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
