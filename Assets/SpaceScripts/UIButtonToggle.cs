using UnityEngine;
using System.Collections;

public class UIButtonToggle : MonoBehaviour {

	public GameObject target;
	public bool state = true;
	
	void OnClick () { 
		state = !NGUITools.GetActive(target);
		if (target != null) 
			NGUITools.SetActive(target, state); 
	}
}
