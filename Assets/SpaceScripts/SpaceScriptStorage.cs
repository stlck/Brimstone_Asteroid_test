using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceScriptStorage : ScriptableObject {

	public List<ShipControlTranslate> ShipList = new List<ShipControlTranslate>();
	public UIPanel ShipMarketTemplate;
    
    public List<ShipControlTranslate> GetUIShipList()
    {
        return ShipList;
    }
}

public class UIShip
{
    public ShipControlTranslate Ship;
    public UIPanel Panel;
}