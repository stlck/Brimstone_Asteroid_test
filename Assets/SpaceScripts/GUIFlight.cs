using UnityEngine;
using System.Collections;

public class GUIFlight : MonoBehaviour {

	SpacePlayerObject playerObject;

	// Use this for initialization
	void Start () {
	}

	void OnLevelWasLoaded(int level)
	{
		playerObject = SpacePlayerObject.Instance;
		playerObject.SpawnShip ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
