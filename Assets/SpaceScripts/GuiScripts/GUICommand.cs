using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GUICommand : MonoBehaviour {

	UISlideTrigger current;
	SpacePlayerObject playerObject;

	// Use this for initialization
	void Start () {
			playerObject = SpacePlayerObject.Instance;
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DepartPressed()
	{
        SpaceGameManager.Instance.LaunchMission();
	}

	public void ShipSelected(string ship)
	{
		if(playerObject != null)
			playerObject.ShipObject = Resources.Load<ShipControlTranslate> ("Ships/" + ship);
	}

	public void WeaponSelected(string weapon)
	{
		if(playerObject != null)
			playerObject.WeaponObject = Resources.Load<WeaponControl> ("Weapons/" + weapon);
	}

}