using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

	StateMachine State = StateMachine.Instance;

	void Awake()
	{

	}

	// Use this for initialization
	void Start () {
		State.Init ();
	}
	
	// Update is called once per frame
	void Update () {
		State.UpdateCurrent ();	
	}

	void OnGUI()
	{
		State.OnGUICurrent ();
	}
}
