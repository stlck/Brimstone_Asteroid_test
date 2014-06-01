using UnityEngine;
using System.Collections;
using System.Linq;
public class UIHangarPanel : MonoBehaviour {

	public UIPopupList ShipList;
	public GameObject ShipOkay;
	public ShipControlTranslate CurrentShip;

	public UISlider Armor;
	public UISlider Rotation;
	public UISlider Speed;
	public UISlider Strafe;
	public UISlider Shield;

	// Use this for initialization
	void Start () {
		//ShipList.items = SpaceGameManager.Instance.GameVariables.OwnedShips.Select( m => m.ShipAttributes.Name).ToList();

		UIEventListener.Get(ShipOkay).onClick += ShipSelect;
		if(SpaceGameManager.Instance.GameVariables.OwnedShips.Any())
			CurrentShip = SpaceGameManager.Instance.GameVariables.OwnedShips.First ();
		
		GetComponent<UISlideTrigger>().OnSlideEvent += (bool state) => {
			ShipList.items = SpaceGameManager.Instance.GameVariables.OwnedShips.Select( m => m.ShipAttributes.Name).ToList();
		};
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setupShip()
	{
		Rotation.sliderValue = CurrentShip.ShipAttributes.SpeedRotate /10;
		Speed.sliderValue = CurrentShip.ShipAttributes.Speed/10;
		Strafe.sliderValue = CurrentShip.ShipAttributes.SpeedStrafe/10;
		Shield.sliderValue = CurrentShip.ShipAttributes.Shield/500;
		Armor.sliderValue = CurrentShip.ShipAttributes.Armor/500;
	}

	void ShipSelected(string ship)
	{
		if (ship == "")
				return;
		var s = SpaceGameManager.Instance.GameVariables.OwnedShips.First (m => m.ShipAttributes.Name == ship);
		CurrentShip = s;
		setupShip ();
	}

	public void ShipSelect(GameObject go)
	{
		SpacePlayerObject.Instance.ShipObject = CurrentShip;//go.GetComponent<ShipControlTranslate> ();//Resources.Load<ShipControlTranslate> ("Ships/" + ship);
	}
}
