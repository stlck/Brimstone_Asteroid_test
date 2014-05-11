using UnityEngine;
using System.Collections;

public class StatePlay : GameState {
	
	protected static StatePlay _instance = new StatePlay();
	
	public static StatePlay Instance
	{
		get
		{
			return _instance;
		}
	}

	public override void OnGUIState ()
	{
		GUILayout.Label ("Playing"); 
		if (GUILayout.Button ("Pause")) {
			StateMachine.SetPause ();
		}	
	}
	
	public override void UpdateState ()
	{

	}
	
	public override void StartState ()
	{
		Time.timeScale = 1;
	}

	public override void EndState ()
	{
		
	}

	public override string ToString ()
	{
		return string.Format ("[StatePlay]");
	}
}
