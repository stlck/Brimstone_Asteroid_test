using UnityEngine;
using System.Collections;

public class ShipControlTranslate : MonoBehaviour {

	public NetworkPlayer owner;
	public float Speed = 1;
	public float StrafeSpeed = 1;
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

			transform.Translate (Vector3.right * horizontalInput * Speed);
			transform.Translate (Vector3.forward * verticalInput * StrafeSpeed);

			var pos = transform.position;
			pos.z = 0;
			transform.position = pos;

			transform.LookAt(target, Vector3.forward * -1);

			if(Network.peerType != NetworkPeerType.Disconnected)
				networkView.RPC ("UpdateMe", RPCMode.Others, pos, transform.rotation);
		}
	}

	[RPC]
	public void UpdateMe(Vector3 pos, Vector3 rot)
	{
		transform.position = pos;
		transform.eulerAngles = rot;
	}
}
