using UnityEngine;
using System.Collections;

public class UISlideTrigger : MonoBehaviour {

	public GameObject TriggerButton;
	public bool State = false;
	Animator _anim;

	void Start()
	{
		_anim = GetComponent< Animator>();
	}

	public void Slide(bool state)
	{
		State = state;
		_anim.SetBool ("Slide", State);
	}
}
