using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class AStarPathFinder : IPathFinder {
	// Source : http://theory.stanford.edu/~amitp/GameProgramming/ImplementationNotes.html

	public List<ATile> GetPath (List<ATile> a_tiles, ATile a_fromTile, ATile a_toTile)
	{
		List<ATile> path = new List<ATile>();

		// Initialize array
		List<AStarWrapperTile> allWrappedTiles = new List<AStarWrapperTile>();
		List<AStarWrapperTile> open = new List<AStarWrapperTile>();
		List<AStarWrapperTile> close = new List<AStarWrapperTile>();

		// Initialize AStarTiles
		AStarWrapperTile destination = null;
		foreach(ATile tile in a_tiles)
		{
			AStarWrapperTile wrappedTile = new AStarWrapperTile(tile);
			allWrappedTiles.Add(wrappedTile);

			if(tile.Equals(a_fromTile))
				open.Add(wrappedTile);				

			if(tile.Equals(a_toTile))
				destination = wrappedTile;
		}

		// Compute Algorithm
		AStarWrapperTile currentNode = null;

		while(open.Count > 0){
			float lowF = -1;
			// find the node with the least f on the open list, call it "q"
			foreach( AStarWrapperTile tile in open)
			{
				if( lowF == -1 || tile.f < lowF )
				{
					lowF = tile.f;
					currentNode = tile;
				}
			}

			if(currentNode == destination)
				break;

			// pop q off the open list
			open.Remove(currentNode);
			// push q on the closed list
			close.Add(currentNode);

			List<Link> connections = currentNode.tile.GetConnections();
			for(int i = 0; i < connections.Count; i++)
			{
				Link link = connections[i];
				AStarWrapperTile wrappedTileNeighboor = GetWrapperFromTile(allWrappedTiles, link.Tile);
				if(wrappedTileNeighboor != null && !wrappedTileNeighboor.tile.isBlocked && !close.Contains(wrappedTileNeighboor))
				{
					float gScore = currentNode.g + currentNode.getCostTo(wrappedTileNeighboor);
					
					if(!open.Contains(wrappedTileNeighboor))
					{
						wrappedTileNeighboor.h = wrappedTileNeighboor.getHeuristicTo(destination);
						open.Add(wrappedTileNeighboor);
						wrappedTileNeighboor.parent = currentNode;
						wrappedTileNeighboor.g = gScore;
						wrappedTileNeighboor.f = wrappedTileNeighboor.g + wrappedTileNeighboor.h;
					}
					else if(gScore < wrappedTileNeighboor.g)
					{
						wrappedTileNeighboor.parent = currentNode;
						wrappedTileNeighboor.g = gScore;
						wrappedTileNeighboor.f = wrappedTileNeighboor.g + wrappedTileNeighboor.h;
					}
				}
			}
		} 

		// Construct final path
		AStarWrapperTile cursor = destination;
		while(cursor != null)
		{
			path.Add(cursor.tile);
			cursor = cursor.parent;
		}

		path.Reverse();
		return path;
	}

	private AStarWrapperTile GetWrapperFromTile(List<AStarWrapperTile> a_allTiles, ATile a_tile)
	{
		foreach(AStarWrapperTile tile in a_allTiles)
		{
			if(tile.tile.Equals(a_tile)) return tile;
		}

		return null;
	}
}
