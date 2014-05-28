using UnityEngine;
using System.Collections;

public class SpacePlayerObject : MonoBehaviour {
    private static SpacePlayerObject _instance;
    public static SpacePlayerObject Instance
    {
        get
        {
            return _instance;
        }
    }

	public NetworkPlayer Owner;
	public ShipControlTranslate ShipObject;
	public WeaponControl WeaponObject;

	ShipControlTranslate spawnedShip;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
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
        spawnedShip.networkView.RPC("SetOwner", RPCMode.Others, spawnedShip.owner);

		var weapon = Instantiate (WeaponObject, spawnedShip.transform.position, Quaternion.identity) as WeaponControl;
		weapon.transform.parent = spawnedShip.transform;
		weapon.transform.rotation = Quaternion.identity;
        
		Camera.main.gameObject.AddComponent<CameraFollow> ().target = spawnedShip.transform;
	}


}
