using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class FloorTeleporter : Floor
{
	#region Inspector Properties
	public Link destination = new Link();
	#endregion

	#region Private Properties
	protected override bool _IsBuildable
	{
		get
		{
			return false;
		}
	}
	#endregion

	public override List<Link> GetConnections ()
	{
		List<Link> connections = new List<Link>();
		foreach(Link l in base.GetConnections())
			connections.Add(l);
		if(destination.Tile != null) connections.Add(destination);
		return connections;
	}

	protected override void OnDrawGizmos ()
	{
		base.OnDrawGizmos ();
		
		Gizmos.color = Color.green;
		
		if(destination.Tile)
		{
			Gizmos.DrawLine(this.transform.position, this.transform.position + (destination.Tile.transform.position - this.transform.position) );
			Gizmos.DrawWireSphere(this.transform.position + (destination.Tile.transform.position - this.transform.position), 0.1f);
		}
	}

	#region Interaction Management
	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<Unit>() != null && destination.Tile != null)
			other.GetComponent<Unit>().OnTeleport(destination.Tile);
	}
	#endregion
}

