using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GUICommand : MonoBehaviour {

	public List<UISlideTrigger> PanelTriggers = new List<UISlideTrigger>();
	public SpaceScriptStorage StorageUnit;
    public UIPanel MarketShips;

	UISlideTrigger current;
	SpacePlayerObject playerObject;

	// Use this for initialization
	void Start () {

		playerObject = SpacePlayerObject.Instance;

		foreach(var p in PanelTriggers)
		    UIEventListener.Get(p.TriggerButton).onClick += TriggerClicked;
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
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SlideClose()
	{
		if (current != null)
			current.Slide (false);
		current = null;
	}

	public void DepartPressed()
	{
        SpaceGameManager.Instance.LaunchMission();
        //SpaceGameManager.Instance.networkView.RPC("", RPCMode.Server, "SpaceFlightScene");

        //Application.LoadLevel ("SpaceFlightScene");
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

    public void ShowShipList()
    {
        var shipPanel = StorageUnit.ShipMarketTemplate;
        var y = 0;
        foreach (var s in StorageUnit.ShipList)
        {
            var p = Instantiate(shipPanel) as UIPanel;
            
            p.GetComponent<UIMarketShipControl>().SetShip(s);
            p.transform.parent = MarketShips.transform;
            p.transform.localScale = Vector3.one;
            p.transform.position = Vector3.zero;
            //var label = NGUITools.AddWidget<UILabel>(MarketShips.gameObject);
            
            //label.text = s.name;
            //var btn = NGUITools.AddWidget<UIButton>(MarketShips.gameObject);
            
            //MarketShips.AddWidget<UILabel>();
        }
    }
}