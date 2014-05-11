using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StateLobby : GameState {
	
	protected static StateLobby _instance = new StateLobby();
	
	public static StateLobby Instance
	{
		get
		{
			return _instance;
		}
	}

	public List<Sprite> ships = new List<Sprite>();

	public override void OnGUIState ()
	{
		GUILayout.Label ("Lobby"); 
		GUILayout.BeginHorizontal ();
		foreach (var s in ships) {
			if(GUILayout.Button(s.texture, GUILayout.Width(120), GUILayout.Height(120)))
			{
				instantiateShip(s);
				StateMachine.SetPlay();
			}
		}
		GUILayout.EndHorizontal ();
	}

	void instantiateShip(Sprite s)
	{
		var go = new GameObject ();
		var ship = go.AddComponent<ShipControl> ();
		ship.name = "SHIP";
		//ship.enabled = false;
		ship.Sprite = s;

		var rid = go.AddComponent<Rigidbody2D> ();
		rid.gravityScale = 0;
		rid.mass = 100;
	}
	
	public override void UpdateState ()
	{
		if (ships.Count == 0) {
			var res = Resources.LoadAll<Sprite>("");
			ships = res.ToList();
		}
	}
	
	public override void StartState ()
	{
		Time.timeScale = 0;
	}
	public override void EndState ()
	{
		
	}

	public override string ToString ()
	{
		return string.Format ("[StateLobby]");
	}
}
