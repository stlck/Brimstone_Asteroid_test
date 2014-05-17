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

	public NetworkPlayer owner;

	// Use this for initialization
	void Start () {
		//var sr = gameObject.AddComponent<SpriteRenderer> ();
		//sr.sprite = this.Sprite;
        manager = GameObject.FindObjectOfType<SimpleGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (owner == Network.player) {
						horizontalInput = Input.GetAxis ("Horizontal")* -1;
						verticalInput = Input.GetAxis ("Vertical");

						if (Input.GetKeyDown (KeyCode.Space) && Bullet != null) {
								if (!Network.isServer)
										networkView.RPC ("Shoot", RPCMode.Server);
								else
										Shoot ();
								//b.rigidbody2D.velocity = transform.forward *5;
						}

						if (!Network.isServer && (verticalInput != 0 || horizontalInput != 0))
								networkView.RPC ("SetINput", RPCMode.Server, verticalInput, horizontalInput);
				} 

		if(Network.isServer)
			move ();
	}
	
	void FixedUpdate()
	{
		if(!Network.isServer)
			return;

		networkView.RPC ("MoveMe", RPCMode.Others, transform.position, transform.eulerAngles, rigidbody2D.velocity.x, rigidbody2D.velocity.y);
		
		//stay in x of game
		if (transform.position.x > play_width || transform.position.x < -play_width)
		{	Vector3 cur_Pos = transform.position;cur_Pos.x = -(cur_Pos.x);transform.position = cur_Pos;}
		
		//stay in y of game
		if (transform.position.y > play_height || transform.position.y < -play_height)
		{	Vector3 cur_Pos = transform.position;cur_Pos.y = -(cur_Pos.y);transform.position = cur_Pos;}

	}

	[RPC]
	public void SetINput(float ver, float hor)
	{
		verticalInput = ver;
		horizontalInput = hor;
	}

	void move()
	{
		var z = transform.TransformDirection(Vector3.right);

		if (verticalInput != 0 && rigidbody2D.velocity.magnitude <= 10)
			rigidbody2D.AddForce ( z * verticalInput * 3000 * Time.deltaTime); //new Vector2(Mathf.Sin(z), Mathf.Cos (z))
		//else if (rigidbody2D.velocity.magnitude > 0)
		//	rigidbody2D.velocity -= rigidbody2D.velocity.normalized * Time.deltaTime;
		
		if (horizontalInput != 0)
			transform.RotateAround ( Vector3.forward, horizontalInput * 2 * Time.deltaTime);

		verticalInput = 0f;
		horizontalInput = 0f;
	}

	[RPC]
	public void MoveMe(Vector3 pos, Vector3 rot, float x, float y)
	{
		transform.position = pos;
		transform.eulerAngles = rot;
		rigidbody2D.velocity = new Vector2 (x, y);
	}

    [RPC]
    public void Shoot()
    {
        Network.Instantiate(Bullet, transform.position + transform.forward + transform.right, transform.rotation, 2);
    }

	[RPC]
	public void SetOwner(NetworkPlayer p)
	{
		owner = p;
	}
}
