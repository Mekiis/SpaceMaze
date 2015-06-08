using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class AFloor : ATile 
{
	#region Inspector Properties
	public List<Link> connection 		= new List<Link>();
	#endregion

	#region Private Properties
	internal Tower tower = null;
	internal GameObject towerObject = null;
	
	protected virtual bool _IsBuildable
	{
		get
		{
			return (this.isWalkable);
		}
	}

	private Material _matSelected = null;
	private Material MatSelected
	{
		get
		{
			if(_matSelected == null)
				_matSelected = Resources.Load("Graphics/Material/FloorSelected", typeof(Material)) as Material;

			return _matSelected;
		}
	}
	private Material _matUnSelected = null;
	private Material MatUnSelected
	{
		get
		{
			if(_matSelected == null)
				_matUnSelected = Resources.Load("Graphics/Material/FloorUnSelected", typeof(Material)) as Material;
			
			return _matUnSelected;
		}
	}
	private Material _matBlocked = null;
	private Material MatBlocked
	{
		get
		{
			if(_matBlocked == null)
				_matBlocked = Resources.Load("Graphics/Material/FloorBlocked", typeof(Material)) as Material;
			
			return _matBlocked;
		}
	}

	protected GameManager _gm;
	#endregion

	public override List<Link> GetConnections ()
	{
		return connection;
	}

	#region State Management
	// Use this for initialization
	void Start () {
		_gm = SGMGameManager.Instance.gameManager as GameManager;
		FloorManager.Instance.RegisterFloor (this);
		GetComponent<Renderer>().material = MatUnSelected;
	}
	#endregion

	protected virtual void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		foreach (Link l in connection) 
		{
			if(l.Tile != null)
			{
				Gizmos.DrawLine(this.transform.position, this.transform.position + (l.Tile.transform.position - this.transform.position) / 2f );
				Gizmos.DrawWireSphere(this.transform.position + (l.Tile.transform.position - this.transform.position) / 2f, 0.1f);
			}
		}

		Gizmos.color = Color.red;
		if(!isWalkable)
			Gizmos.DrawWireSphere(this.transform.position, 0.3f);
	}

	public void BuildNeighboor(float range)
	{
		connection.Clear();
		Floor[] allObjects = FindObjectsOfType<Floor> ();
		foreach (Floor f in allObjects) {
			if(!f.Equals(this) && f.gameObject.transform.up == this.gameObject.transform.up)
			{
				if(Vector3.Distance(f.gameObject.transform.position, this.transform.position) <= range && !(f is FloorTeleporter))
				{
					Link l = new Link();
					l.Tile = f;
					connection.Add(l);
				}
			}
		}
	}
	
	#region Interaction Management
	public virtual void OnSelected()
	{
		if(_IsBuildable)
			GetComponent<Renderer>().material = MatSelected;
		else
			GetComponent<Renderer>().material = MatBlocked;
	}

	public virtual void OnUnSelected()
	{
		GetComponent<Renderer>().material = MatUnSelected;
	}

	public virtual void OnClick()
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
