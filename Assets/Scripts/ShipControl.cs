using UnityEngine;
using System.Collections;

public class ShipControl : MonoBehaviour {

	public Sprite Sprite;
	public Transform Bullet;

	// Use this for initialization
	void Start () {
        //var sr = gameObject.AddComponent<SpriteRenderer> ();
        //sr.sprite = this.Sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if (StateMachine.Instance.Current != StatePlay.Instance)
			return;


	}

	void FixedUpdate()
	{
		if (StateMachine.Instance.Current != StatePlay.Instance)
			return;

		var hor = Input.GetAxis ("Horizontal") * -1;
		var ver = Input.GetAxis ("Vertical");
		var z = transform.TransformDirection(Vector3.right);

		Debug.Log (z);
		if (ver != 0)
			rigidbody2D.AddForce ( z * ver * 100); //new Vector2(Mathf.Sin(z), Mathf.Cos (z))
		else if (rigidbody2D.velocity.magnitude > 0)
			rigidbody2D.velocity -= rigidbody2D.velocity.normalized * .01f;

		if (hor != 0)
			transform.RotateAround ( Vector3.forward, hor * .2f);

		if (Input.GetKeyDown (KeyCode.Space) && Bullet != null) {
			var b = Instantiate(Bullet, transform.position + transform.forward, Bullet.rotation) as Transform;
			b.rigidbody2D.velocity = transform.forward *5;
		}
	}
}
