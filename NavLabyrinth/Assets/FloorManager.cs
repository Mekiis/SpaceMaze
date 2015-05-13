using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class FloorManager : MonoBehaviour {
	private static List<ATile> floors = new List<ATile>();

	private static FloorManager _instance = null;
	internal static FloorManager Instance
	{
		get
		{
			if(_instance == null)
				_instance = new FloorManager();

			return _instance;
		}
	}

	internal void RegisterFloor(ATile a_floor)
	{
		floors.Add (a_floor);
	}

	internal List<ATile> GetPath(Floor a_origin, Floor a_dest)
	{
		AStarPathFinder pathFinder = new AStarPathFinder ();
		pathFinder.tiles = floors;
		return pathFinder.GetPath(a_origin, a_dest);
	}
}
