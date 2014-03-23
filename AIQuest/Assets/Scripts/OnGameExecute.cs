using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class OnGameExecute : MonoBehaviour 
{
	private bool startEnabled = true;
	private bool hardEnabled = true;
	private bool creditsEnabled = true;
	private bool tutorialEnabled = true;
	public Font buttonFont;
	public GUISkin buttonSkin;
	private ADBannerView banner = null;

	void Start() {
//		PlayerPrefs.DeleteAll ();
		hardEnabled = PlayerPrefs.GetInt ("hardEnabled") == 1;
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			banner = new ADBannerView(ADBannerView.Type.Banner, ADBannerView.Layout.Bottom);
			ADBannerView.onBannerWasClicked += OnBannerClicked;
			ADBannerView.onBannerWasLoaded  += OnBannerLoaded;
		}
		Social.localUser.Authenticate (ProcessAuthentication);
	}

	void OnDestroy() {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			ADBannerView.onBannerWasClicked -= OnBannerClicked;
			ADBannerView.onBannerWasLoaded -= OnBannerLoaded;
			banner.visible = false;
		}
	}
	
	void ProcessAuthentication (bool success) {
		if (success) {
			Debug.Log ("Authenticated, checking achievements");
			
			// Request loaded achievements, and register a callback for processing them
			Social.LoadAchievements (ProcessLoadedAchievements);
		}
		else
			Debug.Log ("Failed to authenticate");
	}
	
	// This function gets called when the LoadAchievement call completes
	void ProcessLoadedAchievements (IAchievement[] achievements) {
		if (achievements.Length == 0)
			Debug.Log ("Error: no achievements found");
		else
			Debug.Log ("Got " + achievements.Length + " achievements");
		
		// You can also call into the functions like this
		Social.ReportProgress ("Achievement01", 100.0, result => {
			if (result)
				Debug.Log ("Successfully reported achievement progress");
			else
				Debug.Log ("Failed to report achievement");
		});
	}

	void OnGUI()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Texture backgroundTex = (Texture)Resources.Load ("FullScreenImages/main_screen");
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.font = buttonFont;
		buttonStyle.fontSize = 30;

		GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height), backgroundTex);

		GUI.color = new Color(90f/255f, 220f/255f, 214f/255f, 1f);
		GUI.backgroundColor = Color.white;
		//GUI enabled causes button to disable/enable
		GUI.enabled = startEnabled;
		if (GUI.Button (new Rect (Screen.width * .715f, Screen.height * .76f,Screen.width * .2f,Screen.height * .1f), "Start", buttonStyle)) 
		{
			startEnabled = false;
			hardEnabled = false;
			creditsEnabled = false;
			tutorialEnabled = false;
			if (Application.platform == RuntimePlatform.IPhonePlayer) {banner.visible = false;}
			PlayerPrefs.SetInt("NumberOfPlayers", 0);
			AutoFade.LoadLevel("StartScene",2,1,Color.black);
		}

		//GUI enabled causes button to disable/enable
		if (hardEnabled) {
			GUI.enabled = hardEnabled;
			if (GUI.Button (new Rect (Screen.width * .415f, Screen.height * .76f, Screen.width * .2f, Screen.height * .1f), "Hard", buttonStyle)) {
				startEnabled = false;
				hardEnabled = false;
				creditsEnabled = false;
				tutorialEnabled = false;
				if (Application.platform == RuntimePlatform.IPhonePlayer) {
						banner.visible = false;
				}
				PlayerPrefs.SetInt ("NumberOfPlayers", 1);
				AutoFade.LoadLevel ("StartScene", 2, 1, Color.black);
			}
		}

		//GUI enabled causes button to disable/enable
		GUI.enabled = creditsEnabled;
		if (GUI.Button (new Rect (Screen.width * .05f, Screen.height * .46f, Screen.width * .2f, Screen.height * .1f), "Credits", buttonStyle)) 
		{
			startEnabled = false;
			hardEnabled = false;
			creditsEnabled = false;
			tutorialEnabled =false;
			Application.LoadLevel("CreditScene");
		}

		//GUI enabled cause button to disable/enable
		GUI.enabled = tutorialEnabled;
		if (GUI.Button (new Rect (Screen.width * .05f, Screen.height * .61f,  Screen.width * .2f, Screen.height * .1f), "Tutorial", buttonStyle)) 
		{
			startEnabled = false;
			hardEnabled = false;
			creditsEnabled = false;
			tutorialEnabled = false;
			Application.LoadLevel("TutorialScene");
		}

		//GUI enabled causes button to disable/enable
		if ((Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)) {
			if (GUI.Button (new Rect (Screen.width * .05f, Screen.height * .76f, Screen.width * .2f, Screen.height * .1f), "Scores", buttonStyle))  {
				Social.ShowLeaderboardUI ();
			}
		} else {
			if (GUI.Button (new Rect (Screen.width * .05f, Screen.height * .76f, Screen.width * .2f, Screen.height * .1f), "Exit", buttonStyle))  {
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
