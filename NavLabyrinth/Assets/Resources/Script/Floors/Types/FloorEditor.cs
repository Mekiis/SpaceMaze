using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof( AFloor ), true )]
[CanEditMultipleObjects]
public class FloorEditor : Editor
{
	
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		if(GUILayout.Button("Find neighboor"))
		{
			CallBuild();
		}
	}

	private void CallBuild()
	{
		foreach(Floor f in targets)
		{
			f.BuildNeighboor(2.1f);
		}
	}
}

