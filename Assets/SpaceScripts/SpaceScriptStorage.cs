using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpaceScriptStorage : ScriptableObject {

	public List<ShipControlTranslate> ShipList = new List<ShipControlTranslate>();
	public UIPanel ShipMarketTemplate;

	public ShipControlTranslate GetShipByName(string name)
	{
		if (ShipList.Any (m => m.name == name))
			return ShipList.First (m => m.name == name);
		return null;
	}

	public List<WeaponControl> WeaponList = new List<WeaponControl>();
}

public class UIShip
{
    public ShipControlTranslate Ship;
    public UIPanel Panel;
}