using UnityEngine;
using System.Collections;

public class SpaceNetworkItem : MonoBehaviour {

    public int ID;
    public SpaceItemType ItemType;
    public string Name;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public enum SpaceItemType
{
    Ship = 0,
    Weapon,
    Equipment,
    Pilot,
    Resource,
    Blueprint,
    Building,
    Ammonition
}
