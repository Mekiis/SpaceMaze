using UnityEngine;
using System.Collections;

internal class FloorPath : Floor 
{	
	protected override bool _IsBuildable
	{
		get
		{
			return false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<Unit>() != null)
			other.GetComponent<Unit>().OnDestinationReach(this);
	}

	protected override void OnDrawGizmos ()
	{
		base.OnDrawGizmos ();
		
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(this.transform.position, 0.1f);
	}
}
