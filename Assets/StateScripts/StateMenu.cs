using UnityEngine;
using System.Collections;

public class StateMenu : GameState {

	protected static StateMenu _instance = new StateMenu();

	public static StateMenu Instance
	{
		get
		{
			return _instance;
		}
	}

	public override void OnGUIState ()
	{
		GUILayout.Label ("Menu"); 
		if (GUILayout.Button ("Start")) 
			StateMachine.SetLobby();
		
	}

	public override void UpdateState ()
	{

	}

	public override string ToString ()
	{
		return string.Format ("[StateMenu]");
	}

	public override void StartState ()
	{

	}
	public override void EndState ()
	{

	}
}
