using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

	// Use this for initialization
	private float lifespan_current;
	private float lifespan;

	private float play_width = 8f;
	private float play_height = 6.2f;
	void Start () {
		lifespan = 75;
		lifespan_current = 0;
		transform.rigidbody2D.AddForce ( transform.TransformDirection(Vector3.right) * 500);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// live up to 'lifespan' only
		lifespan_current++;
		if (lifespan_current >= lifespan)
		{
			Destroy(this.gameObject);
		}

		if (transform.position.x > play_width || transform.position.x < -play_width)
		{
			Vector3 cur_Pos = transform.position;
			cur_Pos.x = -(cur_Pos.x);
			transform.position = cur_Pos;
		}

		if(transform.position.y > 6.2f)
		{
			Vector3 cur_Pos = transform.position;
			cur_Pos.y = -(6.2f);
			transform.position = cur_Pos;
		}
		
		if(transform.position.y < -(6.2f))
		{
			Vector3 cur_Pos = transform.position;
			cur_Pos.y = (6.2f);
			transform.position = cur_Pos;
		}	
	}


}
