using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceScriptStorage : ScriptableObject {

	public List<ShipControlTranslate> ShipList = new List<ShipControlTranslate>();
	public UIPanel ShipMarketTemplate;
    
    public List<ShipControlTranslate> GetUIShipList()
    {
        //var ret = new List<UIShip>();

        //foreach(var s in ShipList)
        //{
        //    var newShipPanel = new UIShip() { Ship = s };
        //    newShipPanel.Panel = n
        //    ret.Add();
        //}

        return ShipList;
    }
}

public class UIShip
{
    public ShipControlTranslate Ship;
    public UIPanel Panel;
}