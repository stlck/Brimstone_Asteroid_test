using UnityEngine;
using System.Collections;

public class UIMarketShipControl : MonoBehaviour {

    public UILabel Label;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void SetShip (ShipControlTranslate s) {
        Label.text = s.name;
	}
}
