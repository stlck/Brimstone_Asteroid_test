using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SimpleGameManager : MonoBehaviour {

    public Transform PlayerShip;
    public List<HostData> hostdata = new List<HostData>();
    public string GameTypeName = "BAsteroidsTest";
    enum LobbyState
    {
        lobby,
        Hosting,
        Joined,
        Playing
    }
    LobbyState lobbyState;

	// Use this for initialization
	void Start () {
        //Manager = GameObject.FindObjectOfType<GameStateManager>();
        MasterServer.ClearHostList();
        MasterServer.RequestHostList(GameTypeName);
   
        lobbyState = LobbyState.lobby;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.Label("Lobby");

        switch (lobbyState)
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
            case LobbyState.Playing:
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

    void RefreshLobby()
    {
        hostdata = MasterServer.PollHostList().ToList();
    }

    void StartGame()
    {
        MasterServer.UnregisterHost();
        networkView.RPC("RPCStart", RPCMode.AllBuffered);
    }

    void HostGame()
    {
        Network.InitializeServer(32, 7777, !Network.HavePublicAddress());
        MasterServer.RegisterHost(GameTypeName, "Test1");
    }

    [RPC]
    void RPCStart()
    {
        InstantiateShip();
        lobbyState = LobbyState.Playing;
    }

    public void InstantiateShip()
    {
        if (Network.peerType != NetworkPeerType.Disconnected)
            Network.Instantiate(PlayerShip, Vector3.zero, Quaternion.identity, 0);
        else
            Transform.Instantiate(PlayerShip);
    }
}
