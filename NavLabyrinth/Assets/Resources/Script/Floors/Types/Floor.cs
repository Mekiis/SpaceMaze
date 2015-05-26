using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class Floor : ATile 
{
	public List<Link> connection 		= new List<Link>();

	internal Tower tower = null;
	
	private bool IsOccuped
	{
		get
		{
			return (this.isBlocked || tower != null);
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

	private GameManager _gm;

	// Use this for initialization
	void Start () {
		_gm = SGMGameManager.Instance.gameManager as GameManager;
		FloorManager.Instance.RegisterFloor (this);
		GetComponent<Renderer>().material = MatUnSelected;
	}

	public override List<Link> GetConnections ()
	{
		return connection;
	}

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
		if(isBlocked)
			Gizmos.DrawWireSphere(this.transform.position, 0.3f);
	}

	public void BuildNeighboor(float range)
	{
		connection.Clear();
		Floor[] allObjects = FindObjectsOfType<Floor> ();
		foreach (Floor f in allObjects) {
			if(!f.Equals(this) && f.gameObject.transform.up == this.gameObject.transform.up)
			{
				if(Vector3.Distance(f.gameObject.transform.position, this.transform.position) <= range)
				{
					Link l = new Link();
					l.Tile = f;
					connection.Add(l);
				}
			}
		}
	}

	public virtual void OnSelected()
	{
		if(!isBlocked)
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
		if(!isBlocked)
		{
			List<Tower> towersAvaible = _gm.towerManager.GetEvolutionOfTower(tower);
			Debug.Log("Tower avaible for this tile : "+towersAvaible.Count);
		}
	}
}
