using UnityEngine;
using System.Collections;

public class StateLoading : GameState {
	
	protected static StateLoading _instance = new StateLoading();
	
	public static StateLoading Instance
	{
		get
		{
			return _instance;
		}
	}
	
	public override void OnGUIState ()
	{
		GUILayout.Label ("Loading"); 
		
	}
	
	public override void UpdateState ()
	{
		
	}
	
	public override void StartState ()
	{
		
	}
	public override void EndState ()
	{
		
	}
	
	public override string ToString ()
	{
		return string.Format ("[StateLoading]");
	}
}