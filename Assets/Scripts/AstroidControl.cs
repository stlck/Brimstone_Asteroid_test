using UnityEngine;
using System.Collections;

public class AstroidControl : MonoBehaviour {
	
	private float play_width = 8f;
	private float play_height = 6f;

	// Use this for initialization
	void Start () {
		if(Network.peerType != NetworkPeerType.Server)
			return;


		Vector3 start_post = new Vector3();
		float X_or_Y = Random.Range(0f, 1f);
		if (X_or_Y < 0.5)
			start_post = new Vector3(Random.Range(-play_width, play_width),play_height,0);
		else
			start_post = new Vector3(play_width,Random.Range(-play_height, play_height),0);

		transform.position = start_post;

		// set speed
		transform.Rotate(new Vector3(Random.Range(1f, 360f), Random.Range(1f, 360f), 0));
		transform.rigidbody2D.AddForce ( transform.forward * 150);
	
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		if(Network.peerType != NetworkPeerType.Server)
			return;
		//stay in x of game
		if (transform.position.x > play_width || transform.position.x < -play_width)
		{	Vector3 cur_Pos = transform.position;cur_Pos.x = -(cur_Pos.x);transform.position = cur_Pos;}
		
		//stay in y of game
		if (transform.position.y > play_height || transform.position.y < -play_height)
		{	Vector3 cur_Pos = transform.position;cur_Pos.y = -(cur_Pos.y);transform.position = cur_Pos;}
	}
	void OnCollisionEnter2D(Collision2D collision) {
		if(Network.peerType != NetworkPeerType.Server)
			return;
		if (collision.gameObject.tag == "Bullet") 
		{
            if (Network.peerType != NetworkPeerType.Disconnected)
            {
                Network.Destroy(collision.gameObject);
                Network.Destroy(this.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
		}
	}
}
