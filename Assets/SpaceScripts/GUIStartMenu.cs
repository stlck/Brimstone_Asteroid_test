using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GUIStartMenu : MonoBehaviour {

	private static GUIStartMenu _instance;
	public static GUIStartMenu Instance
	{
		get{
			return _instance;
		}
	}

	public UIPopupList GameList;

	// Use this for initialization
	void Start () {
		if (_instance == null)
			_instance = this;
		else
			Destroy (gameObject);

	}

	public UIInput PlayerNameLabel;
	public string GetPlayerName
	{
		get{
			return PlayerNameLabel.text;
		}
	}

	public delegate void StartButtonClickEvent ();
	public event StartButtonClickEvent OnStartButtonClick;
	public void StartPressed()
	{
		if (OnStartButtonClick != null)
			OnStartButtonClick ();
	}

	public delegate void GameJoinedEvent(HostData data);
	public event GameJoinedEvent OnGameJoined;
	public void GameJoinedPressed()
	{
		HostData data = null;
		Debug.Log (GameList.selection);
		var selection = GameList.selection;
		if ( selection == "None" || selection == null)
			return;

		data = recievedHostData.First (m => m.gameName == selection);
		if (OnGameJoined != null)
			OnGameJoined (data);
	}

	List<HostData> recievedHostData;
	public delegate List<HostData> RefreshDataEvent();
	public event RefreshDataEvent OnRefreshData;
	public void RefreshData()
	{
		if (OnRefreshData != null) {
			recievedHostData = OnRefreshData();
			GameList.items = recievedHostData.Select( m => m.gameName).ToList();
		}
	}

	public void QuitPressed()
	{
		Application.Quit ();
	}
}
