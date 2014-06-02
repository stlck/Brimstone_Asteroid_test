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
    public GUICommand GuiCommand;
    public GUIFlight GuiFlight;
	public SpaceScriptStorage StorageUnit;
	
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
		public string CurrentMission;
		public bool InFlight;
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
            SetPlayer(Network.player, "Server", "", false);
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

	void OnLevelWasLoaded(int i)
	{
		if (Network.isServer)
			AddCash( 200);
	}

	public void BuyShip(ShipControlTranslate s)
	{
		if (GameVariables.Cash >= s.ShipAttributes.Price) {
			AddCash(- s.ShipAttributes.Price);
			//GameVariables.Cash -= s.ShipAttributes.Price;
			networkView.RPC ("BuyShipNetworked", RPCMode.All, s.name); 
		}
	}

	[RPC]
	public void BuyShipNetworked(string name)
	{
		GameVariables.OwnedShips.Add (StorageUnit.GetShipByName(name));		
	}

	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player " + Network.connections.Length + " connected from " + player.ipAddress + ":" + player.port);

        GameVariables.PlayersInGame.Add(new SpaceGamePlayer() { Player = player,  Name = "Client " + Network.connections });

        foreach (var p in GameVariables.PlayersInGame)
            networkView.RPC("SetPlayer", RPCMode.Others, p.Player, p.Name, "", false);
	}

    [RPC]
	public void SetPlayer(NetworkPlayer player, string name, string mission, bool inflight)
    {
        if (!GameVariables.PlayersInGame.Any (m => m.Player != null && m.Player == player))
			GameVariables.PlayersInGame.Add (new SpaceGamePlayer () { Player = player, Name = player.ipAddress, CurrentMission = "", InFlight = false });
		else {
			var p = GameVariables.PlayersInGame.First (m => m.Player == player);//.Name = name;
			p.Name = name;
			p.CurrentMission = mission;
			p.InFlight = inflight;
		}
    }

	public void MissionCompleted(MissionObject misison)
	{
		SpaceGameManager.Instance.AddCash (misison.CashReward);
		
	}

	public void AddCash(int amount)
	{
		if (Network.peerType == NetworkPeerType.Server)
			AddCashNetworked (amount);
		else
			networkView.RPC ("AddCashNetworked", RPCMode.Server, amount);
	}

	[RPC]
	public void AddCashNetworked(int amount)
	{
		GameVariables.Cash += amount;
		networkView.RPC ("SetCash", RPCMode.All, GameVariables.Cash);
	}
    
    [RPC]
    public void SetCash(int amount)
    {
        GameVariables.Cash = amount;
		GuiCommand.SetCashLabel ("$$ " + amount);
    }

    public void LaunchMission()
    {
        PlayerObject.SpawnShip();

        GuiCommand.gameObject.SetActive(false);
        GuiFlight.gameObject.SetActive(true);
    }
}
