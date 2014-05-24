using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

	public PlayerObject Player;
	public List<TradingPost> TradingPosts;
	public ShipDatabase ShipDatabase;

	TradingShip selectedShip;
	TradingPost selectedPost;

	// Use this for initialization
	void Start () {
		Player.ListOfShips.Add (ShipDatabase.AllShips [0]);
		Player.CurrentLocation = TradingPosts.First ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		// ships
		// posts
		GUILayout.BeginArea (new Rect (10, 10, 100, 100));
		GUILayout.Label (Player.Stats.Name);
		GUILayout.Label ("$$: " + Player.Stats.Cash);
		GUILayout.EndArea ();

		GUILayout.BeginArea (new Rect (10, 110, 300, Screen.height - 130));
		GUILayout.Box ("ships", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
		GUILayout.EndArea ();
		
		GUILayout.BeginArea (new Rect (20, 130, 280, Screen.height - 150));
		foreach (var ship in Player.ListOfShips) {
			GUILayout.Label (ship.Name);
			
			if( ship.ShipIcon != null)
				GUILayout.Label (ship.ShipIcon);

			if( ship.Route != null){
				GUILayout.Label(ship.Route.Start.ToString());
				GUILayout.Label(ship.RoutePathComplete + "/100");
				GUILayout.Label(ship.Route.End.ToString());
			}

			if(ship.CurrentGoods != null)
			foreach(var g in ship.CurrentGoods)
				GUILayout.Label(g.Name);
		}
		GUILayout.EndArea ();
	
		GUILayout.BeginArea (new Rect (310, 110, 300, Screen.height - 130));
		GUILayout.Box ("Posts", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
		GUILayout.EndArea ();

		GUILayout.BeginArea (new Rect (320, 130, 300, Screen.height - 130));
		foreach (var post in TradingPosts) {
			GUILayout.Label(post.ToString());
		}
		GUILayout.EndArea ();
	}
}
