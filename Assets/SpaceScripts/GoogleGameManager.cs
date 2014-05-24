using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GoogleGameManager : MonoBehaviour {

	private const float FontSizeMult = 0.025f;
	private bool mWaitingForAuth = false;
	private string mStatusText = "Ready.";

	// Use this for initialization
	void Start () {
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();


	}
	
	// Update is called once per frame
	void Update () {
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
		});
	}

	void OnGUI() {
		GUI.skin.button.fontSize = (int)(FontSizeMult * Screen.height);
		GUI.skin.label.fontSize = (int)(FontSizeMult * Screen.height);
		
		GUI.Label(new Rect(220, 20, Screen.width, Screen.height * 0.25f), mStatusText);
		
		if (mWaitingForAuth) {
			return;
		}
		
		string buttonLabel = Social.localUser.authenticated ? "Sign Out" : "Authenticate";
		Rect buttonRect = new Rect(0.25f * Screen.width, 0.25f * Screen.height,
		                           0.5f * Screen.width, 0.5f * Screen.height);
		
		if (GUI.Button(buttonRect, buttonLabel)) {
			if (!Social.localUser.authenticated) {
				// Authenticate
				mWaitingForAuth = true;
				mStatusText = "Authenticating...";
				Social.localUser.Authenticate((bool success) => {
					mWaitingForAuth = false;
					mStatusText = success ? "Successfully authenticated" : "Authentication failed.";
				});
			} else {
				// Sign out!
				mStatusText = "Signing out.";
				((GooglePlayGames.PlayGamesPlatform) Social.Active).SignOut();
			}
		}
	}
}
