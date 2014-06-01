using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CreateMissionTargets : MonoBehaviour {

	public List<Transform> Targets = new List<Transform>();
	public MissionControl Mission;
	List<Transform> instantiatedTargets = new List<Transform>();

	// Use this for initialization
	void Start () {
		Mission = GetComponent<MissionControl> ();

		foreach (var t in Targets)
			instantiatedTargets.Add (Instantiate (t) as Transform);

	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		if (instantiatedTargets.All (m => m == null))
			Mission.Completed ();
	}
}
