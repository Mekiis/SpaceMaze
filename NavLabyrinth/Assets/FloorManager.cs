using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class FloorManager : MonoBehaviour {
	private List<Floor> floors = new List<Floor>();

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

	internal void RegisterFloor(Floor a_floor)
	{
		floors.Add (a_floor);
	}

	internal List<Floor> GetPath(Floor a_origin, Floor a_dest)
	{
		List<Floor> path = new List<Floor> ();

		// Todo Compute A* algorithm
		path.Add (a_origin);
		path.Add (a_dest);

		return path;
	}
}
