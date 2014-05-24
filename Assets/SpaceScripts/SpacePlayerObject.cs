using UnityEngine;
using System.Collections;

public class SpacePlayerObject : MonoBehaviour {

	private static SpacePlayerObject _instance;
	public static SpacePlayerObject Instance
	{
		get{
			return _instance;
		}
	}

	public ShipControlTranslate ShipObject;
	ShipControlTranslate spawnedShip;
	// Use this for initialization
	void Start () {
		if (_instance == null)
			_instance = this;
		else
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnShip()
	{
		if (spawnedShip != null)
			return;

		if (Network.peerType == NetworkPeerType.Disconnected)
			spawnedShip = Instantiate (ShipObject) as ShipControlTranslate;
		else
			spawnedShip = Network.Instantiate(ShipObject, Vector3.zero, ShipObject.transform.rotation, 0) as ShipControlTranslate;

		spawnedShip.owner = Network.player;
	}


}
