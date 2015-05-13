using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class AStarPathFinder : APathFinder {
	// Source : http://theory.stanford.edu/~amitp/GameProgramming/ImplementationNotes.html

	public override List<ATile> GetPath (ATile a_fromTile, ATile a_toTile)
	{
		List<ATile> path = new List<ATile>();

		// Initialize array
		List<AStarWrapperTile> allWrappedTiles = new List<AStarWrapperTile>();
		List<AStarWrapperTile> open = new List<AStarWrapperTile>();
		List<AStarWrapperTile> close = new List<AStarWrapperTile>();

		// Initialize AStarTiles
		AStarWrapperTile destination = null;
		foreach(ATile tile in tiles)
		{
			AStarWrapperTile wrappedTile = new AStarWrapperTile(tile);
			Debug.Log ("Wrapping : "+wrappedTile);
			allWrappedTiles.Add(wrappedTile);

			if(tile.Equals(a_fromTile))
			{
				open.Add(wrappedTile);
				wrappedTile.cost = 0f;
			}
				

			if(tile.Equals(a_toTile))
				destination = wrappedTile;
		}

		// Build Neightboor
		foreach(AStarWrapperTile wrappedTile in allWrappedTiles)
		{
			foreach(ATile tileNeightboor in wrappedTile.tile.connection)
			{
				wrappedTile.neightboors.Add(GetWrapperFromTile(allWrappedTiles, tileNeightboor));
			}
		}

		// Compute Algorithm
		AStarWrapperTile bestNode = null;

		do{
			float lowestCost = -1;
			// find the node with the least f on the open list, call it "q"
			foreach( AStarWrapperTile tile in open)
			{
				if( lowestCost == -1 || tile.cost < lowestCost )
				{
					lowestCost = tile.cost;
					bestNode = tile;
				}
			}

			// pop q off the open list
			open.Remove(bestNode);
			// push q on the closed list
			close.Add(bestNode);

			foreach( AStarWrapperTile neightboor in bestNode.neightboors )
			{
				float cost = bestNode.cost + bestNode.getCostTo(neightboor) + neightboor.getHeuristicTo(destination);

				if(cost < neightboor.total)
				{
					if(open.Contains(neightboor)) 
					   	open.Remove(neightboor);
					else if(close.Contains(neightboor))
					    close.Remove(neightboor);
					else
					{
						neightboor.cost = cost;
						neightboor.parent = bestNode;
						open.Add(neightboor);
					}
				}
			}

		} while(open.Count != 0);

		// Construct final path
		AStarWrapperTile cursor = destination;
		while(cursor != null && cursor.tile != a_fromTile)
		{
			path.Add(cursor.tile);
			cursor = cursor.parent;
		}

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
