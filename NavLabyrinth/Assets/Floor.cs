using UnityEngine;
using System.Collections;

internal class Floor : ATile 
{
	// Use this for initialization
	void Start () {
		FloorManager.Instance.RegisterFloor (this);
	}
	
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.magenta;
		foreach (Floor f in connection) 
		{
			if(f != null)
				Gizmos.DrawLine(this.transform.position, f.transform.position);
		}
			
	}
}
