using UnityEngine;
using System.Collections;

internal class FloorEnd : Floor 
{	
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
