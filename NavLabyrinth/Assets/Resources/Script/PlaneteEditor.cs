using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof( Planete ), true )]
public class PlaneteEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();
		
		if(GUILayout.Button("Fill atributs with childrens"))
		{
			Planete p = (Planete) target;
			p.FillAttributsWithChidrens();
		}
	}
}

