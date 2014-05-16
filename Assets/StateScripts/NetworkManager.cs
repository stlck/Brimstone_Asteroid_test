using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class NetworkManager : MonoBehaviour {

    public SimpleGameManager Manager;
    public PlayerProps MyProps;
    public List<PlayerProps> Others = new List<PlayerProps>();

    // Use this for initialization
	void Start () {
        //if (MyProps == null)
        {
            
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 200, 10, 200, 200));
        switch (Manager.lobbyState)
        {
            case SimpleGameManager.LobbyState.lobby:
                GUILayout.Label("NAME");
                MyProps.Name = GUILayout.TextField(MyProps.Name);
                break;
        }

        
        foreach (var prop in Others)
            GUILayout.Label(prop.Name);

        GUILayout.EndArea();
    }

    void OnPlayerConnected(NetworkPlayer player) 
    {
        //if(Network.isServer)
		foreach (var p in Others) {
				
			Debug.Log (p.Player + " " + p.Name);
			networkView.RPC ("GetMyProps", player, p.Player, p.Id, p.Name);
		}
    }

    void OnConnectedToServer()
    {
		MyProps = new PlayerProps();
		MyProps.Player = Network.player;
		MyProps.Id = networkView.viewID;
		MyProps.Name = "NamedPlayer";
		Others.Add(MyProps);

        networkView.RPC("GetMyProps", RPCMode.Server, Network.player, MyProps.Id, MyProps.Name);
    }

    [RPC]
    public void GetMyProps(NetworkPlayer p, NetworkViewID id,string n)
    {
		Debug.Log (p + " " + n);
        if(!Others.Any(m => m.Player == p))
        {
            Others.Add(new PlayerProps() {Player = p, Id = id, Name = n });
        }
    }
}

[Serializable]
public class PlayerProps
{
	public NetworkPlayer Player;
    public NetworkViewID Id;
    public string Name;

}