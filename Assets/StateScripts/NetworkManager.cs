﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NetworkManager : MonoBehaviour {

    public SimpleGameManager Manager;
    public PlayerProps MyProps;
    public List<PlayerProps> Others = new List<PlayerProps>();

    // Use this for initialization
	void Start () {
        if (MyProps == null)
        {
            MyProps = new PlayerProps();
            MyProps.Id = networkView.viewID;
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
            case SimpleGameManager.LobbyState.Joined:
                GUILayout.Label(MyProps.Name);
                foreach(var prop in Others)
                    GUILayout.Label(prop.Name);
                break;
            case SimpleGameManager.LobbyState.Playing:
                GUILayout.Label(MyProps.Name);
                foreach(var prop in Others)
                    GUILayout.Label(prop.Name);
                break;
        }
        GUILayout.EndArea();
    }

    void OnConnectedToServer()
    {
        networkView.RPC("GetMyProps", RPCMode.OthersBuffered, MyProps.Id, MyProps.Name);
    }

    [RPC]
    public void GetMyProps(NetworkViewID id,string n)
    {
        if(!Others.Any(m => m.Id == id))
        {
            Others.Add(new PlayerProps() { Id = id, Name = n });
        }
    }
}

public class PlayerProps
{
    public NetworkViewID Id;
    public string Name;

}