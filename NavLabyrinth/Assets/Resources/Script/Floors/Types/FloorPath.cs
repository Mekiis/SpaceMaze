using UnityEngine;
using System.Collections;

internal class FloorPath : Floor 
{	
	#region Private Properties
	protected override bool _IsBuildable
	{
		get
		{
			return false;
		}
	}
	#endregion

	protected override void OnDrawGizmos ()
	{
		base.OnDrawGizmos ();
		
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(this.transform.position, 0.1f);
	}

	#region Interaction Management
	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<Unit>() != null)
			other.GetComponent<Unit>().OnDestinationReach(this);
	}
	#endregion
}
