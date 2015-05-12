using UnityEngine;
using System.Collections;

internal class AStarWrapperTile : ATile
{
	internal ATile tile = null;
	internal ATile parent = null;
	internal float cost = 0f;

	public AStarWrapperTile(ATile a_tile)
	{
		this.tile = a_tile;
	}

	internal float getCostTo(AStarWrapperTile tile)
	{
		return 0f;
	}
}

