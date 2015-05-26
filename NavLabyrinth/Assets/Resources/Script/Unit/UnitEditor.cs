using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof( Unit ), true )]
[CanEditMultipleObjects]
public class UnitEditor : Editor
{
	
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		Unit u = target as Unit;
		if(GUI.changed)
		{
			target.name = u.id;
		}
	}
}

