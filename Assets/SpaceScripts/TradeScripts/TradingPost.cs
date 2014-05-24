using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

[Serializable]
public class TradingPost {

	public string Name;
	public List<Good> ListOfAvaiableGoods;
	public Vector2 Location;

	public override string ToString ()
	{
		var ret = string.Format ("[{0}]{1}",Location,Name);

		foreach (var g in ListOfAvaiableGoods)
			ret += "\n\t" + g.Name + ": $" + g.BasePrice;

		return ret;
	}
}
