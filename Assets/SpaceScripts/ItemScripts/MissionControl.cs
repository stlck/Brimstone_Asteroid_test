using UnityEngine;
using System.Collections;
using System;

public class MissionControl : MonoBehaviour {

	public MissionObject MissionObject;

	// Use this for initialization
	void Start () {
		if (MissionObject.ActivateComponents)
			foreach (var comp in GetComponents<MonoBehaviour>())
				if (!comp.enabled)
						comp.enabled = true;
	}
	
	// Update is called once per frame
	public void Completed () {
		SpaceGameManager.Instance.MissionCompleted (MissionObject);//.AddCash (MissionObject.CashReward);
		if (Network.peerType == NetworkPeerType.Disconnected)
			Destroy (gameObject);
		else
			Network.Destroy (gameObject);
	}
}

[Serializable]
public class MissionObject
{
	public string Name;
	public MissionType Type;
	public Vector3 Location;
	public bool ActivateComponents = true;

	public int CashReward;
}

public enum MissionType
{
	MINE,
	KILL,
	DEFEND,
	RETRIEVE
	//More
}