using UnityEditor;
using UnityEngine;

public class Renaming : MonoBehaviour
{
	[MenuItem ("SGM/Batch Rename")]
	static void BatchRename()
	{
		string iname = "";
		string ispacer = "0";
		int icount = 0;
		//int istuff = Selection.gameObjects.Length; // if I wanted this to support renaming of > 99 objects correctly, I'd use this.

		iname = Selection.activeGameObject.name; // The item in the inspector

		foreach (GameObject igo in Selection.gameObjects)
		{
			icount++;
			if (icount > 9) ispacer = "";
			igo.name = iname + "_" + ispacer + icount;
		}

	}
}
