using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal abstract class APathFinder : MonoBehaviour {
	public List<ATile> tiles = new List<ATile>();

	public abstract List<ATile> GetPath(ATile fromTile, ATile toTile);
}
