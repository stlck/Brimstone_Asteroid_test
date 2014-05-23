using UnityEngine;
using System.Collections;

public class UISlideInPanel: MonoBehaviour {
	
	public UISlideTrigger target;
	public bool UseToggle = true;
	public bool State = false;

	void OnClick()
	{
		if (UseToggle) {
			target.State = !target.State;
			target.Slide ();
		} 
		else if( target.State != State){
			target.State = State;
			target.Slide ();
		}
	}

}


