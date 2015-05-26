using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof( Tower ), true )]
[CanEditMultipleObjects]
public class TowerEditor : Editor
{
	
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		Tower t = target as Tower;
		if(GUI.changed)
		{
			target.name = t.id;
		}
	}
}

