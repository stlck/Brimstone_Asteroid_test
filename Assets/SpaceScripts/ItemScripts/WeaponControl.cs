using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour {

	public BulletControl Ammonition;
	public float ShotCooldown = 1f;

	bool canShoot = false;

	// Use this for initialization
	void Start () {
		StartCoroutine (waitAndShoot ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && canShoot)
			Shoot ();
	}

	IEnumerator waitAndShoot()
	{
		while (true) {
			yield return new WaitForSeconds(ShotCooldown);
			canShoot = true;
		}
	}

	public void Shoot()
	{
		canShoot = false;
		if (Ammonition == null)
				return;
		if (Network.peerType == NetworkPeerType.Disconnected)
			Instantiate (Ammonition, transform.root.position + transform.forward, transform.rotation);
		else
            Network.Instantiate(Ammonition, transform.root.position + transform.forward, transform.root.rotation, 0);
	}
}
