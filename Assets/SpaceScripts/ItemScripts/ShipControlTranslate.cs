using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShipControlTranslate : MonoBehaviour {

	public NetworkPlayer owner;
	public ShipAttributes ShipAttributes = new ShipAttributes();
	//public float Speed = 1;
	//public float StrafeSpeed = 1;
	Camera cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Network.player == owner || Network.peerType == NetworkPeerType.Disconnected) 
		{
			var target = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

			var horizontalInput = Input.GetAxis ("Horizontal") * Time.deltaTime;
			var verticalInput = Input.GetAxis ("Vertical") * Time.deltaTime;

			transform.Translate (Vector3.right * horizontalInput * ShipAttributes.SpeedStrafe);
			transform.Translate (Vector3.forward * verticalInput * ShipAttributes.Speed);

			var pos = transform.position;
			pos.z = 0;
			transform.position = pos;

			transform.LookAt(target, Vector3.forward * -1);

			if(Network.peerType != NetworkPeerType.Disconnected)
				networkView.RPC ("UpdateMe", RPCMode.Others, pos, transform.eulerAngles);
		}
	}

	[RPC]
	public void UpdateMe(Vector3 pos, Vector3 rot)
	{
		transform.position = pos;
		transform.eulerAngles = rot;
	}

    [RPC]
    void SetOwner(NetworkPlayer o)
    {
        this.owner = o;
    }
}
[Serializable]
public class ShipAttributes
{
	public string Name;
	[Range(0,10)]
	public float Speed = 1;
	[Range(0,10)]
	public float SpeedStrafe = 1;
	[Range(0,10)]
	public float SpeedRotate = 1;
	[Range(0,500)]
	public float Shield = 100;
	[Range(0,500)]
	public float Armor = 100;

	public int Price = 100;

	public List<WeaponControl> Weapons = new List<WeaponControl>();
	// TODO: Equipment
}

