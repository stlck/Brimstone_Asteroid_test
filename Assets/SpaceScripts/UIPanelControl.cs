using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIPanelControl : MonoBehaviour {
	public List<UISlideTrigger> PanelTriggers = new List<UISlideTrigger>();
	UISlideTrigger current;
	
	// Use this for initialization
	void Start () {
		foreach(var p in PanelTriggers)
			UIEventListener.Get(p.TriggerButton).onClick += TriggerClicked;
	}
	
	public void SlideClose()
	{
		if (current != null)
			current.Slide (false);
		current = null;
	}
	
	public void TriggerClicked(GameObject go)
	{
		Debug.Log (go + " trigger");
		if (current != null)
			current.Slide (false);
		
		if(PanelTriggers.Any( m => m.TriggerButton == go)){
			
			current = PanelTriggers.First (m => m.TriggerButton == go);
			current.Slide(true);
		}
	}
}
