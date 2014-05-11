using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.rigidbody2D.AddForce ( transform.TransformDirection(Vector3.right) * 200);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
}
