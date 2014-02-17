using UnityEngine;
using System.Collections;

public class OnCreditExecute : MonoBehaviour
{
	public Font buttonFont;
	private ADBannerView banner;

	void Start() {
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

	void OnGUI()
	{
		Texture backgroundTex = (Texture)Resources.Load ("FullScreenImages/credits");
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.font = buttonFont;
		buttonStyle.fontSize = 30;
		Rect sceneRect = new Rect (0, 0, Screen.width, Screen.height);

		GUI.DrawTexture (sceneRect, backgroundTex);

		GUI.color = new Color(90f/255f, 220f/255f, 214f/255f, 1f);
		GUI.backgroundColor = Color.white;
		if (GUI.Button(new Rect(Screen.width * .725f, Screen.height * .9f, Screen.width * .15f, Screen.height * .07f), "back", buttonStyle)) 
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {banner.visible = false;}
			AutoFade.LoadLevel("OnGameLaunchScene",0,.5f,Color.black);
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

