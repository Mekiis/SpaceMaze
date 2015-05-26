using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class TowerManager : MonoBehaviour {
	public List<Tower> towers = new List<Tower>();

	public List<Tower> GetEvolutionOfTower(Tower a_tower)
	{
		List<Tower> towerEvolutions = new List<Tower>();

		foreach(Tower tower in towers)
		{
			if(tower.parents.Contains(a_tower))
				towerEvolutions.Add(tower);
		}

		return towerEvolutions;
	}
}
