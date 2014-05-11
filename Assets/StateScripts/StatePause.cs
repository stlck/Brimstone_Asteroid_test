using UnityEngine;
using System.Collections;

public class StatePause : GameState {
	
	protected static StatePause _instance = new StatePause();
	
	public static StatePause Instance
	{
		get
		{
			return _instance;
		}
	}

	public override void OnGUIState ()
	{
		GUILayout.Label ("Pause"); 
		if (GUILayout.Button ("Play"))
			StateMachine.SetPlay ();
			
	}
	
	public override void UpdateState ()
	{
		
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
		return string.Format ("[StatePause]");
	}
}
