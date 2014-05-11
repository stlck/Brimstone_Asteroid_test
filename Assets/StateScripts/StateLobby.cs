﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StateLobby : GameState {
	
	protected static StateLobby _instance = new StateLobby();
	
	public static StateLobby Instance
	{
		get
		{
			return _instance;
		}
	}

    //public List<Sprite> ships = new List<Sprite>();
    public List<HostData> hostdata = new List<HostData>();
    public string GameTypeName = "BAsteroidsTest";
    enum LobbyState
    {
        lobby,
        Hosting,
        Joined
    }
    LobbyState lobbyState;
    public GameStateManager Manager;

	public override void OnGUIState ()
	{
		GUILayout.Label ("Lobby");

        switch(lobbyState)
        {
            case LobbyState.lobby:
                if (GUILayout.Button("Refresh"))
                    RefreshLobby();
                if (GUILayout.Button("Host"))
                {
                    lobbyState = LobbyState.Hosting;
                    HostGame();
                }
                showLobby();
                break;
            case LobbyState.Hosting:
                if (GUILayout.Button("Start"))
                {
                    StartGame(); 
                }
                break;
            case LobbyState.Joined:
                GUILayout.Label("Waiting");
                break;
        }

        if (GUILayout.Button("Quit"))
            Application.Quit();
	}

    void showLobby()
    {
        foreach (var hd in hostdata)
        {
            GUILayout.BeginHorizontal();
            var name = hd.gameName + " " + hd.connectedPlayers + " / " + hd.playerLimit;
            GUILayout.Label(name);
            GUILayout.Space(5);
            var hostInfo = "[";
            foreach (var host in hd.ip)
                hostInfo = hostInfo + host + ":" + hd.port + " ";
            hostInfo = hostInfo + "]";
            GUILayout.Label(hostInfo);
            GUILayout.Space(5);
            GUILayout.Label(hd.comment);
            GUILayout.Space(5);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Connect"))
            {
                lobbyState = LobbyState.Joined;
                Network.Connect(hd);
            }
            GUILayout.EndHorizontal();
        }
    }

	void instantiateShip(Sprite s)
	{
		var go = new GameObject ();
		var ship = go.AddComponent<ShipControl> ();
		ship.name = "SHIP";
		//ship.enabled = false;
		ship.Sprite = s;

		var rid = go.AddComponent<Rigidbody2D> ();
		rid.gravityScale = 0;
		rid.mass = 100;
	}
	
	public override void UpdateState ()
	{

	}
	
	public override void StartState ()
	{
        Manager = GameObject.FindObjectOfType<GameStateManager>();
        MasterServer.ClearHostList();
        MasterServer.RequestHostList(GameTypeName);
		Time.timeScale = 0;
        lobbyState = LobbyState.lobby;
	}

	public override void EndState ()
	{
		
	}

	public override string ToString ()
	{
		return string.Format ("[StateLobby]");
	}

    void RefreshLobby()
    {
        hostdata = MasterServer.PollHostList().ToList();
    }

    void StartGame()
    {
        MasterServer.UnregisterHost();
        Manager.networkView.RPC("RPCStart", RPCMode.AllBuffered);
    }

    void HostGame()
    {
        Network.InitializeServer(32, 25002, !Network.HavePublicAddress());
        MasterServer.RegisterHost(GameTypeName, "Test1");
        
    }
}
