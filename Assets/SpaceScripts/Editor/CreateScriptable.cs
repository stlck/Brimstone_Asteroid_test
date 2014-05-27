using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateScriptable : UnityEditor.Editor {

	[MenuItem("Assets/Create/SpaceStorage")]
	public static void CreateAsset ()
	{
		var storage = new SpaceScriptStorage ();
		AssetDatabase.CreateAsset(storage, "Assets/SpaceAssets/SpaceStorage.asset");
		// Print the path of the created asset
		Debug.Log(AssetDatabase.GetAssetPath(storage));
	}
}
