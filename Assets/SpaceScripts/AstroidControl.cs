﻿using UnityEngine;
using System.Collections;

public class AstroidControl : MonoBehaviour {
	
	private float play_width = 8f;
	private float play_height = 6f;
    private SimpleGameManager manager;
    public int Size = 3;

	// Use this for initialization
	void Start () {
		transform.Rotate(new Vector3(Random.Range(1f, 360f), Random.Range(1f, 360f), 0));

		if(Network.peerType != NetworkPeerType.Server)
			return;

        manager = GameObject.FindObjectOfType<SimpleGameManager>();

        if (Size == 3)
        {
            Vector3 start_post = new Vector3();
            float X_or_Y = Random.Range(0f, 1f);
            if (X_or_Y < 0.5)
                start_post = new Vector3(Random.Range(-play_width, play_width), play_height, 0);
            else
                start_post = new Vector3(play_width, Random.Range(-play_height, play_height), 0);

            transform.position = start_post;
        }

		// set speed
		transform.rigidbody2D.AddForce ( transform.forward * 150);
	
	}



	// Update is called once per frame
	void FixedUpdate () {
		
		if(Network.peerType != NetworkPeerType.Server)
			return;

		//stay in x of game
		//if (transform.position.x > play_width || transform.position.x < -play_width)
		//{	Vector3 cur_Pos = transform.position;cur_Pos.x = -(cur_Pos.x);transform.position = cur_Pos;}
		
		//stay in y of game
		//if (transform.position.y > play_height || transform.position.y < -play_height)
		//{	Vector3 cur_Pos = transform.position;cur_Pos.y = -(cur_Pos.y);transform.position = cur_Pos;}

		networkView.RPC ("MveMe", RPCMode.All,transform.position, rigidbody2D.velocity.x, rigidbody2D.velocity.y);
	}

	[RPC]
	public void MveMe(Vector3 pos, float x, float y)
	{
		transform.position = pos;
		rigidbody2D.velocity = new Vector2(x,y);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(Network.peerType != NetworkPeerType.Server)
			return;
		if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player") 
		{
            if (Network.peerType != NetworkPeerType.Disconnected)
            {
                //manager.SpawnAsteroid(this);

				if(collision.gameObject.networkView != null)
                	Network.Destroy(collision.gameObject);
				else
                	Destroy(collision.gameObject);
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
