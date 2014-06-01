using UnityEngine;
using System.Collections;

public class UIMarketScript : MonoBehaviour {

	public SpaceScriptStorage StorageUnit;
    public UIGrid MarketShips;

	// Use this for initialization
	void Start () {
		SetupShipList ();
		GetComponent<UISlideTrigger> ().OnSlideEvent += (bool state) => {
			GetComponent<UIPanelControl>().SlideClose();		
		};
	}
	
	public void SetupShipList()
	{
		var shipPanel = StorageUnit.ShipMarketTemplate;
		
		foreach (var s in StorageUnit.ShipList)
		{
			var sp = NGUITools.AddChild(MarketShips.gameObject, shipPanel.gameObject);
			var ship = sp.GetComponent<UIMarketShipControl>();
			ship.SetShip( s);
			ship.OnShipBought += (ShipControlTranslate m) => 
			(
					SpaceGameManager.Instance.BuyShip(m)
			);
		}
		MarketShips.repositionNow = true;
	}
}
