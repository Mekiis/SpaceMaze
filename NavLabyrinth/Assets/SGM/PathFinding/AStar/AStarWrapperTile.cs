using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class AStarWrapperTile
{
	internal ATile tile = null;
	internal List<AStarWrapperTile> neightboors = new List<AStarWrapperTile>();
	internal AStarWrapperTile parent = null;

	internal float cost = float.MaxValue;
	internal float heuristic = float.MaxValue;
	internal float total = float.MaxValue;

	public AStarWrapperTile(ATile a_tile)
	{
		this.tile = a_tile;
	}

	internal float getCostTo(AStarWrapperTile tile)
	{
		return 1f;
	}

	internal float getHeuristicTo(AStarWrapperTile tile)
	{
		return 1f;
	}
}

