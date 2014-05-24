using UnityEngine;
using System.Collections;

public class ShipControlTranslate : MonoBehaviour {

	public NetworkPlayer owner;
	Camera cam;
	[Range(0,3)]
	float horVelocity = 1;
	[Range(0,3)]
	float verVelocity = 1;

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

			transform.Translate (Vector3.right * horizontalInput);
			transform.Translate (Vector3.forward * verticalInput);
			
			transform.LookAt(target, Vector3.forward * -1);

			if(Network.peerType != NetworkPeerType.Disconnected)
				networkView.RPC ("UpdateMe", RPCMode.Others, transform.position, transform.rotation);
		}
	}

	[RPC]
	public void UpdateMe(Vector3 pos, Vector3 rot)
	{
		transform.position = pos;
		transform.eulerAngles = rot;
	}
}
