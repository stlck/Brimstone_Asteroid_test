﻿using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {
	
	// Use this for initialization
	private float lifespan_current;
	private float lifespan;
	
	private float play_width = 8f;
	private float play_height = 6.2f;
	void Start () {
		if(Network.peerType != NetworkPeerType.Server)
			return;

		lifespan = 75;
		lifespan_current = 0;
		transform.rigidbody2D.AddForce ( transform.TransformDirection(Vector3.right) * 500);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Network.peerType != NetworkPeerType.Server)
			return;
		// live up to 'lifespan' only
		lifespan_current++;
		if (lifespan_current >= lifespan)
		{
			Destroy(this.gameObject);
		}
		
		//stay in x of game
		if (transform.position.x > play_width || transform.position.x < -play_width)
		{	Vector3 cur_Pos = transform.position;cur_Pos.x = -(cur_Pos.x);transform.position = cur_Pos;}
		
		//stay in y of game
		if (transform.position.y > play_height || transform.position.y < -play_height)
		{	Vector3 cur_Pos = transform.position;cur_Pos.y = -(cur_Pos.y);transform.position = cur_Pos;}
		
		
	}
	
	
}
