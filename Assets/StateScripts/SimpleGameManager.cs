using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SimpleGameManager : MonoBehaviour {

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
                    StartGame();
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
        Network.InitializeServer(32, 8888, !Network.HavePublicAddress());
        MasterServer.RegisterHost(GameTypeName, "Test1");
    }

    [RPC]
    public void RPCStart()
    {
        lobbyState = LobbyState.Playing;
        
        if (Network.isServer)
        {
			foreach(var p in networkMan.Others)
			{
				InstantiateShip(p.Player);
			}
			

            Targets.Add(Network.Instantiate(Asteroids[0], new Vector3(5, 0, 0), Quaternion.identity, 1) as Transform);
            Targets.Add(Network.Instantiate(Asteroids[0], new Vector3(-5, 3, 0), Quaternion.identity, 1) as Transform);
            Targets.Add(Network.Instantiate(Asteroids[0], new Vector3(0, -3, 0), Quaternion.identity, 1) as Transform);
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
}
