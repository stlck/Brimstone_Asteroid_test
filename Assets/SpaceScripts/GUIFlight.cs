using UnityEngine;
using System.Collections;

public class GUIFlight : MonoBehaviour {

	SpacePlayerObject playerObject;
    public UILabel PlayerName;


	// Use this for initialization
	void Start () {
        PlayerName.text = PlayerPrefs.GetString("PlayerName");
	}

	void OnLevelWasLoaded(int level)
	{

		//playerObject = SpacePlayerObject.Instance;
		playerObject.SpawnShip ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
