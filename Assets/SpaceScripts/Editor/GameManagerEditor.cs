using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor {

	bool toggleGUI = false;
	bool dragging = false;
	TradingPost selected;

	public override void OnInspectorGUI ()
	{
		toggleGUI = EditorGUILayout.Toggle ("Show in Editor", toggleGUI);

		base.OnInspectorGUI ();
	}

	void OnSceneGUI()
	{
		if (toggleGUI) {
			var _t = target as GameManager;
			
			foreach(var post in _t.TradingPosts)
			{
				Debug.Log(post.ToString());
				Handles.Label(post.Location + Vector2.right + Vector2.up*-1, post.Name);
				if (Handles.Button(post.Location + Vector2.right, Quaternion.identity, 2, 1, Handles.SphereCap))
					selected = post;

				post.Location = Handles.FreeMoveHandle(post.Location, Quaternion.identity, 1, Vector3.one, Handles.CircleCap);
			}
		}
	}
}
