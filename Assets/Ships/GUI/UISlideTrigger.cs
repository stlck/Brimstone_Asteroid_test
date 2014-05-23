using UnityEngine;
using System.Collections;

public class UISlideTrigger : MonoBehaviour {

	public bool State = false;
	Animator _anim;

	void Start()
	{
		_anim = GetComponent< Animator>();
	}

	public void Slide()
	{
		_anim.SetTrigger ("Slide");
	}
}
