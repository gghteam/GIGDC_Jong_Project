using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Wolf))]

public class FOVEditor : Editor
{
	private void OnSceneGUI()
	{
		Wolf fov = (Wolf)target;
		Vector2 fromAngle = fov.CirclePoint(-fov.viewAngle * 0.5f);
		Handles.color = Color.white;
		Handles.DrawWireDisc(fov.transform.position, Vector3.forward, fov.viewRange);
		Handles.color = new Color(1, 1, 1, 0.2f);
		Handles.DrawSolidArc(fov.transform.position, Vector3.forward, fromAngle, fov.viewAngle, fov.viewRange);
		//GUIStyle style = new GUIStyle();
		//style.fontSize = 35;
		//Handles.Label(fov.transform.position + new Vector3(0, 0.5f, 0), fov.viewAngle.ToString(), style);
	}
}
