using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

[CustomPropertyDrawer(typeof(Good))]
public class GoodProperrtyDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.Popup (position, 0, GoodsDatabase.AllGameGoods.Select (m => m.Name).ToArray());
	}
}
