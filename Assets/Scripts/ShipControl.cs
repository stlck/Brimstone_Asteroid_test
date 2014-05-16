using UnityEngine;
using System.Collections;

public class ShipControl : MonoBehaviour {
	
	public Sprite Sprite;
	public Transform Bullet;
	private float play_width = 8f;
	private float play_height = 6f;
    SimpleGameManager manager;
	float horizontalInput = 0f;
	float verticalInput = 0f;

	// Use this for initialization
	void Start () {
		//var sr = gameObject.AddComponent<SpriteRenderer> ();
		//sr.sprite = this.Sprite;
        manager = GameObject.FindObjectOfType<SimpleGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (networkView.viewID.isMine) {
			horizontalInput = Input.GetAxis ("Horizontal") * -1;
			verticalInput = Input.GetAxis ("Vertical");

			if(!Network.isServer)
				networkView.RPC ("MoveMe", RPCMode.Server, horizontalInput, verticalInput);
		}
	}
	
	void FixedUpdate()
	{
		if(!Network.isServer)
			return;

		var z = transform.TransformDirection(Vector3.right);
		
		if (verticalInput != 0)
			rigidbody2D.AddForce ( z * verticalInput * 100); //new Vector2(Mathf.Sin(z), Mathf.Cos (z))
		else if (rigidbody2D.velocity.magnitude > 0)
			rigidbody2D.velocity -= rigidbody2D.velocity.normalized * .01f;
		
		if (horizontalInput != 0)
			transform.RotateAround ( Vector3.forward, horizontalInput * .1f);
		
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


		verticalInput = 0f;
		horizontalInput = 0f;
	}

	[RPC]
	public void MoveMe(float hor, float ver)
	{
		Debug.Log (hor + " " + ver + " " + networkView.viewID.owner);
		verticalInput = ver;
		horizontalInput = hor;
	}

    [RPC]
    public void Shoot()
    {
        Network.Instantiate(Bullet, transform.position + transform.forward + transform.right, transform.rotation, 2);
    }
}
