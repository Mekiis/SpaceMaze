using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class AStarWrapperTile
{
	internal ATile tile = null;
	internal AStarWrapperTile parent = null;

	internal float f = 0f;
	internal float g = 0f;
	internal float h = 0f;

	public AStarWrapperTile(ATile a_tile)
	{
		this.tile = a_tile;
	}

	internal float getCostTo(AStarWrapperTile a_tileDestination)
	{
		if(a_tileDestination == null)
			return 0f;

		foreach(Link link in tile.GetConnections())
		{
			if(link != null && link.Tile == a_tileDestination.tile)
				return link.Weight + tile.weight;
		}

		return tile.weight * Vector3.Distance(tile.transform.position, a_tileDestination.tile.transform.position);
	}

	internal float getHeuristicTo(AStarWrapperTile a_tile)
	{
		return 0f;
	}
}

