using UnityEngine;
using System.Collections;

public class UIMissionPanel : MonoBehaviour {

	public GameObject MissionLabel;
	public GameObject grid;
	public SpaceScriptStorage storage;

	// Use this for initialization
	void Start () {
		storage.MissionList.ForEach(delegate(MissionControl obj) {
			var lbl = NGUITools.AddChild(grid, MissionLabel);
			lbl.transform.localScale = lbl.transform.localScale * 14;
			lbl.GetComponent<UILabel>().text = obj.name;
			UIEventListener.Get(lbl).onClick += SelectMission;
			//EventDelegate.Add(button.onClick, YourFunc);

		});
		grid.GetComponent<UIGrid> ().repositionNow = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SelectMission(GameObject mission)
	{
		Debug.Log ("TEST");
		SpaceGameManager.Instance.LaunchMission();
	}
}
