using UnityEngine;
using System.Collections;

public class ShipControl : MonoBehaviour {
	
	public Sprite Sprite;
	public Transform Bullet;
	private float play_width = 8f;
	private float play_height = 6f;
    SimpleGameManager manager;
	
	// Use this for initialization
	void Start () {
		//var sr = gameObject.AddComponent<SpriteRenderer> ();
		//sr.sprite = this.Sprite;
        manager = GameObject.FindObjectOfType<SimpleGameManager>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate()
	{
		
		if(!networkView.viewID.isMine)
			return;

		var hor = Input.GetAxis ("Horizontal") * -1;
		var ver = Input.GetAxis ("Vertical");
		var z = transform.TransformDirection(Vector3.right);
		
		
		if (ver != 0)
			rigidbody2D.AddForce ( z * ver * 100); //new Vector2(Mathf.Sin(z), Mathf.Cos (z))
		else if (rigidbody2D.velocity.magnitude > 0)
			rigidbody2D.velocity -= rigidbody2D.velocity.normalized * .01f;
		
		if (hor != 0)
			transform.RotateAround ( Vector3.forward, hor * .03f);
		
		if (Input.GetKeyDown (KeyCode.Space) && Bullet != null) {
            if(!Network.isServer)
                networkView.RPC("Shoot", RPCMode.Server);
            else
                Shoot();
			//b.rigidbody2D.velocity = transform.forward *5;
		}
		
		
		//stay in x of game
		if (transform.position.x > play_width || transform.position.x < -play_width)
		{	Vector3 cur_Pos = transform.position;cur_Pos.x = -(cur_Pos.x);transform.position = cur_Pos;}
		
		//stay in y of game
		if (transform.position.y > play_height || transform.position.y < -play_height)
		{	Vector3 cur_Pos = transform.position;cur_Pos.y = -(cur_Pos.y);transform.position = cur_Pos;}
		
	}

    [RPC]
    public void Shoot()
    {
        Network.Instantiate(Bullet, transform.position + transform.forward, transform.rotation, 2);
    }
}
