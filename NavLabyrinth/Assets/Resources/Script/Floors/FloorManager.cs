using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class FloorManager : MonoBehaviour {
	private static List<ATile> floors = new List<ATile>();

	internal static FloorManager Instance;
	void Awake()
	{
		if(Instance != null)
			GameObject.Destroy(Instance);
		else
			Instance = this;
		
		DontDestroyOnLoad(this);
	}

	internal void RegisterFloor(ATile a_floor)
	{
		floors.Add (a_floor);
	}

	internal List<ATile> GetPath(ATile a_origin, ATile a_dest)
	{
		AStarPathFinder pathFinder = new AStarPathFinder ();
		return pathFinder.GetPath(floors, a_origin, a_dest);
	}
}
