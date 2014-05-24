using UnityEngine;
using System.Collections;

public class GUICommand : MonoBehaviour {

	SpacePlayerObject playerObject;

	// Use this for initialization
	void Start () {
		playerObject = SpacePlayerObject.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DepartPressed()
	{
		Application.LoadLevel ("SpaceFlightScene");
	}

	public void ShipSelected(string ship)
	{
		if(playerObject != null)
		playerObject.ShipObject = Resources.Load<ShipControlTranslate> ("Ships/" + ship);
	}
}