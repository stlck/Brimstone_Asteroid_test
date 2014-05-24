using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

[Serializable]
public class TradingShip {

	public string Name;
	public Texture2D ShipIcon;

	public TradeRoute Route {
		get;
		set;
	}
	public float RoutePathComplete {
		get;
		set;
	}
	public bool Returning {
		get;
		set;
	}
	public List<Good> CurrentGoods {
		get;
		set;
	}
}
