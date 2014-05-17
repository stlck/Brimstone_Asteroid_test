using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class SimpleGameManager : MonoBehaviour {

	public SimpleGameManager Manager;
	public PlayerProps MyProps;
	public List<PlayerProps> Players = new List<PlayerProps>();
	public bool Spawn = true;

    public List<AstroidControl> Asteroids = new List<AstroidControl>();
    public Transform PlayerShip;
    public List<HostData> hostdata = new List<HostData>();
    public string GameTypeName = "BAsteroidsTest";
    public enum LobbyState
    {
        lobby,
        Hosting,
        Joined,
        Playing,
        WON
    }
    public LobbyState lobbyState;
	NetworkManager networkMan;
    List<Transform> Targets = new List<Transform>();

	// Use this for initialization
	void Start () {
        //Manager = GameObject.FindObjectOfType<GameStateManager>();
        MasterServer.ClearHostList();
        MasterServer.RequestHostList(GameTypeName);
		networkMan = GetComponent<NetworkManager> ();
        lobbyState = LobbyState.lobby;
		MyProps = new PlayerProps();
		MyProps.Name = "1234";
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
				GUILayout.Label("NAME");
				MyProps.Name = GUILayout.TextField(MyProps.Name);
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
			Spawn = GUILayout.Toggle(Spawn, "Spawn asteroids");
                if (GUILayout.Button("Start"))
                    StartGame();
                break;
            case LobbyState.Joined:
                GUILayout.Label("Waiting");
				showWaiting();
                break;
            case LobbyState.Playing:
                break;
        }

        if (GUILayout.Button("Quit"))
            Application.Quit();
    }

	void showWaiting()
	{
		GUILayout.BeginArea(new Rect(Screen.width - 200, 10, 200, 200));
		
		foreach (var prop in Players)
			GUILayout.Label(prop.Name);
		
		GUILayout.EndArea();
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
        MasterServer.RequestHostList(GameTypeName);
    }

    void StartGame()
    {
        MasterServer.UnregisterHost();
        networkView.RPC("RPCStart", RPCMode.AllBuffered);
    }

    void HostGame()
    {
        Network.InitializeServer(32, 8888, !Network.HavePublicAddress());
        MasterServer.RegisterHost(GameTypeName, "Test1");

		Players.Add(MyProps);
    }

	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if(msEvent == MasterServerEvent.HostListReceived)
        	hostdata = MasterServer.PollHostList().ToList();
	}

    [RPC]
    public void RPCStart()
    {
        lobbyState = LobbyState.Playing;
        
        if (Network.isServer)
        {
			foreach(var p in Players)
				InstantiateShip(p.Player);
			if(Network.peerType == NetworkPeerType.Disconnected)
				InstantiateShip(Network.player);

			if(Spawn){
	            Targets.Add(Network.Instantiate(Asteroids[0], new Vector3(5, 0, 0), Quaternion.identity, 1) as Transform);
	            Targets.Add(Network.Instantiate(Asteroids[0], new Vector3(-5, 3, 0), Quaternion.identity, 1) as Transform);
	            Targets.Add(Network.Instantiate(Asteroids[0], new Vector3(0, -3, 0), Quaternion.identity, 1) as Transform);
			}
        }
    }

    public void InstantiateShip(NetworkPlayer owner)
    {
        if (Network.peerType != NetworkPeerType.Disconnected)
		{    
			var ship = Network.Instantiate(PlayerShip, Vector3.zero, Quaternion.identity, 0) as Transform;
			ship.networkView.RPC("SetOwner", RPCMode.All, owner);
		}
        else
            Transform.Instantiate(PlayerShip);
    }

    [RPC]
    public void Victory()
    {
        lobbyState = LobbyState.WON;    
    }

    public void SpawnAsteroid(AstroidControl destroyed)
    {
        if (destroyed.Size == 1) 
            return;

        var go = Asteroids.First(m => m.Size == destroyed.Size - 1);
        Targets.Add(Network.Instantiate(go.transform, destroyed.transform.position, Quaternion.identity, 1) as Transform);
        Targets.Add(Network.Instantiate(go.transform, destroyed.transform.position, Quaternion.identity, 1) as Transform);
    }

	[RPC]
	public void GetMyProps(NetworkPlayer p, string n)
	{
		if(!Players.Any(m => m.Player == p))
		{
			Players.Add(new PlayerProps() {Player = p, Name = n });
		}
	}

	void OnPlayerConnected(NetworkPlayer player) 
	{
		foreach (var p in Players) {
			networkView.RPC ("GetMyProps", player, p.Player, p.Name);
		}
	}
	
	void OnServerInitialized()
	{
		MyProps.Player = Network.player;
	}
	
	void OnConnectedToServer()
	{
		MyProps.Player = Network.player;
		
		networkView.RPC("GetMyProps", RPCMode.Server, Network.player, MyProps.Name);
	}
}

[Serializable]
public class PlayerProps
{
	public NetworkPlayer Player;
	public string Name;
	
}