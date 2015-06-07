using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
internal class Path 
{
	public FloorPath spawnTiles = null;
	public FloorPath destinationTile = null;
}

internal class FieldManager : MonoBehaviour
{
	public List<Path> paths = new List<Path>();
}

