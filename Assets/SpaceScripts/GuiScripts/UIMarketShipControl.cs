using UnityEngine;
using System.Collections;

public class UIMarketShipControl : MonoBehaviour {

	ShipControlTranslate target;
    public UILabel Label;

	public delegate void ShipBought(ShipControlTranslate s);
	public event ShipBought OnShipBought;

	// Update is called once per frame
	public void SetShip ( ShipControlTranslate s) {
		target = s;
        Label.text = s.name;
	}

	public void Pressed()
	{
		if (OnShipBought != null)
			OnShipBought (target);
	}
}
