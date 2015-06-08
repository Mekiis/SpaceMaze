using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class Floor : AFloor 
{
	#region Inspector Properties
	public bool isBuildable 			= true;
	#endregion

	#region Private Properties
	protected override bool _IsBuildable
	{
		get
		{
			return (base._IsBuildable && this.isBuildable);
		}
	}
	#endregion

	
	public override List<Link> GetConnections ()
	{
		return connection;
	}

	#region Interaction Management
	public override void OnClick()
	{
		if(_IsBuildable)
		{
			List<Tower> towersAvaible = _gm.towerManager.GetEvolutionOfTower(tower);
			Debug.Log("Tower avaible for this tile : "+towersAvaible.Count);
			if(towersAvaible.Count > 0)
			{
				if(towerObject != null)
					DestroyImmediate(towerObject);
				tower = towersAvaible[0];
				towerObject = Instantiate(tower.gameObject, this.transform.position, this.transform.rotation) as GameObject;
			}
		}
	}
	#endregion
}
