using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpaceStartMenu : MonoBehaviour {

	public GUIStartMenu guiStartMenu;
	public string GameTypeName = "AsteroidsSpaceTest";
	public string PlayerName;

	// Use this for initialization
	void Start () {
		guiStartMenu.OnGameJoined += HandleOnGameJoined;
		guiStartMenu.OnRefreshData += HandleOnRefreshData;
		guiStartMenu.OnStartButtonClick += HandleOnStartButtonClick;
	}

	void HandleOnStartButtonClick ()
	{
		Debug.Log ("Start");

		MasterServer.RegisterHost ("AsteroidsSpaceTest", "test1234");
		Network.InitializeServer (4, 7007, !Network.useProxy);
		Application.LoadLevel ("SpaceCommandMenu");
	}

	public List<HostData> HandleOnRefreshData ()
	{
		Debug.Log ("Refresh");
		MasterServer.RequestHostList (GameTypeName);
		var ret = MasterServer.PollHostList ().ToList();
		return ret;
	}

	void HandleOnGameJoined (HostData data)
	{
		Debug.Log ("GAmeJoined");
		Network.Connect (data);
		Application.LoadLevel ("SpaceCommandMenu");
	}
	
	// Update is called once per frame
	void Update () {
		PlayerName = guiStartMenu.GetPlayerName;
	}
}
