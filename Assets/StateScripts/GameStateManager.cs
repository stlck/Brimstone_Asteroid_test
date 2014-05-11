using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour {

    public Transform PlayerShipPrefab;

	public StateMachine State = StateMachine.Instance;

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
