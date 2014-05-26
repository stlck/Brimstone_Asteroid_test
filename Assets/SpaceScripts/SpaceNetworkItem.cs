using UnityEngine;
using System.Collections;

public class SpaceNetworkItem : MonoBehaviour {

    public string ID;
    public SpaceItemType ItemType;
    public string Name;
    public NetworkPlayer Owner;

	// Use this for initialization
	void Start () {
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
