using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
internal class Path 
{
	public ATile spawnTiles = null;
	public FloorEnd destinationTile = null;
}

internal class FieldManager : MonoBehaviour
{
	public List<Path> paths = new List<Path>();
}

