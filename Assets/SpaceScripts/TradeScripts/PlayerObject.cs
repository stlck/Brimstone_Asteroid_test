using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

[Serializable]
public class PlayerObject {

	public TradingPost CurrentLocation;
	public List<TradingShip> ListOfShips;
	public PlayerStats Stats;
}

[Serializable]
public class PlayerStats
{
	public int Cash;
	public string Name;
}