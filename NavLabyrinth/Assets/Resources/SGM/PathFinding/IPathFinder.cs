using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal interface IPathFinder {

	List<ATile> GetPath(List<ATile> a_tiles, ATile fromTile, ATile toTile);
}
