using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class AStarPathFinder : APathFinder {
	// Source : http://theory.stanford.edu/~amitp/GameProgramming/ImplementationNotes.html

	public override List<ATile> GetPath (ATile fromTile, ATile toTile)
	{
		List<ATile> path = new List<ATile>();

		// Initialize array
		List<AStarWrapperTile> allTiles = new List<AStarWrapperTile>();
		List<AStarWrapperTile> open = new List<AStarWrapperTile>();
		List<AStarWrapperTile> close = new List<AStarWrapperTile>();

		// Initialize AStarTiles
		AStarWrapperTile destination = null;
		foreach(ATile tile in tiles)
		{
			AStarWrapperTile starTile = new AStarWrapperTile(tile);
			if(tile == fromTile)
				open.Add(starTile);
			else
				allTiles.Add(starTile);

			if(tile == toTile)
				destination = starTile;
		}

		// Compute Algorithm
		AStarWrapperTile previousTile = open[0];
		AStarWrapperTile bestNode = null;
		float lowestCost = -1;
		do{
			foreach( AStarWrapperTile tile in open)
			{
				if( lowestCost == -1 || tile.cost < lowestCost )
				{
					lowestCost = tile.cost;
					bestNode = tile;
				}
			}

			if(bestNode != destination)
			{
				open.Remove(bestNode);
				close.Add(bestNode);
			}

			foreach( AStarWrapperTile neightboor in bestNode.connection )
			{
				float cost = previousTile.cost + bestNode.getCostTo(neightboor);

				if(open.Contains(neightboor) && cost < neightboor.cost)
				{
					open.Remove(neightboor);
				}
				else if(close.Contains(neightboor) && cost < neightboor.cost)
				{
					close.Remove(neightboor);
				}
				else if(!open.Contains(neightboor) && !close.Contains(neightboor))
				{
					/*
					set g(neighbor) to cost
						add neighbor to OPEN
							set priority queue rank to g(neighbor) + h(neighbor)
							set neighbor's parent to current
							*/
				}
			}

		} while(bestNode != destination);

		// Construct final path

		return path;
	}
}
