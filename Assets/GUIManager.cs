using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	private static GUIManager instance;
	
	public static GUIManager Instance
	{
	get{
		if(instance == null)
			instance = new GUIManager();
			return instance;
		}
	}

	public string PlayerLabelText;
	public UILabel PlayerNameLabel;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
