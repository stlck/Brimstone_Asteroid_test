using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceGameManager : MonoBehaviour {

	public SpacePlayerObject PlayerTemplate;
	public List<SpacePlayerObject> PlayerObjects = new List<SpacePlayerObject>();

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnConnectedToServer()
	{
		var PO = Network.Instantiate (PlayerTemplate, Vector3.zero, Quaternion.identity, 0) as SpacePlayerObject;
		PO.Owner = Network.player;
		PO.networkView.RPC ("SetOwner", RPCMode.OthersBuffered, Network.player);
	}

	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player " + Network.connections.Length + " connected from " + player.ipAddress + ":" + player.port);
	}
}
