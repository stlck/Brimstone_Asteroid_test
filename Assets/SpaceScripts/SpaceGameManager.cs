using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class SpaceGameManager : MonoBehaviour {

    private static SpaceGameManager _instance;
    public static SpaceGameManager Instance
    {
        get{
            return _instance;
        }
    }

    //public SpacePlayerObject PlayerTemplate;
    public SpacePlayerObject PlayerObject = new SpacePlayerObject();
    public SpaceGameVariables GameVariables = new SpaceGameVariables();
    public UIRoot GuiCommand;
    public UIRoot GuiFlight;

	[Serializable]
	public class SpaceGameVariables
    {
        public int Cash;
        public List<ShipControlTranslate> OwnedShips = new List<ShipControlTranslate>();
        public List<SpaceGamePlayer> PlayersInGame = new List<SpaceGamePlayer>();
    }

	[Serializable]
	public class SpaceGamePlayer
    {
        public NetworkPlayer Player;
        public string Name;
    }

	void Awake()
	{
		if (_instance == null)
			_instance = this;
		else
			Destroy(gameObject);
		
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
        if (Network.isServer)
            SetPlayer(Network.player, "Server");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnConnectedToServer()
	{
        //var PO = Network.Instantiate (PlayerTemplate, Vector3.zero, Quaternion.identity, 0) as SpacePlayerObject;
        //PO.Owner = Network.player;
        //PO.networkView.RPC ("SetOwner", RPCMode.OthersBuffered, Network.player);
	}

	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player " + Network.connections.Length + " connected from " + player.ipAddress + ":" + player.port);

        GameVariables.PlayersInGame.Add(new SpaceGamePlayer() { Player = player,  Name = "Client " + Network.connections });

        foreach (var p in GameVariables.PlayersInGame)
            networkView.RPC("SetPlayer", RPCMode.Others, p.Player, p.Name);
	}

    [RPC]
    public void SetPlayer(NetworkPlayer player, string name)
    {
        if (!GameVariables.PlayersInGame.Any(m => m.Player != null && m.Player == player))
            GameVariables.PlayersInGame.Add(new SpaceGamePlayer() { Player = player, Name = player.ipAddress });
        else
            GameVariables.PlayersInGame.First(m => m.Player == player).Name = name;
    }

    
    [RPC]
    public void SetCash(int amount)
    {
        GameVariables.Cash = amount;
    }

    public void LaunchMission()
    {
        PlayerObject.SpawnShip();

        GuiCommand.gameObject.SetActive(false);
        GuiFlight.gameObject.SetActive(true);
    }
}
