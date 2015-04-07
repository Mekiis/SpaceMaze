using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor : MonoBehaviour {
	public List<Floor> connection = new List<Floor>();

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
