using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class Floor : ATile 
{
	public List<Link> connection 		= new List<Link>();

	// Use this for initialization
	void Start () {
		FloorManager.Instance.RegisterFloor (this);
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
}
