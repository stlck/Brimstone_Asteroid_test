using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//transform.LookAt (target);

		var pos = target.position;
		//var newPOs = Vector3.Lerp (transform.position, pos, Time.deltaTime);
		pos.z = -10;
		transform.position = pos;
	}
}
