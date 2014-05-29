using UnityEngine;
using System.Collections;

public class UISlideTrigger : MonoBehaviour {

	public GameObject TriggerButton;
	public bool State = false;

	public delegate void OnSlide(bool state);
	public event OnSlide OnSlideEvent;
	Animator _anim;

	void Start()
	{
		_anim = GetComponent< Animator>();
	}

	public void Slide(bool state)
	{
		State = state;
		_anim.SetBool ("Slide", State);

		if (OnSlideEvent != null)
			OnSlideEvent (state);
	}
}
