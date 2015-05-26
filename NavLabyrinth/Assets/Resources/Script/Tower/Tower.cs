using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class Tower : MonoBehaviour {
	public string id = "";
	public string displaybleName = "";
	[Tooltip("Unity unit")]
	public float range = 10f;
	[Tooltip("Number of attack / Second")]
	public float speed = 1f;
	[Tooltip("Dammage deal")]
	public float dammage = 1f;
	public List<Tower> parents = new List<Tower>();

	protected virtual void OnDrawGizmos() {
		Gizmos.color = Color.magenta;

		SphereCollider collider = GetComponentInChildren<SphereCollider>();
		if(collider != null)
		{
			collider.radius = range;
			Gizmos.DrawWireSphere(collider.center, collider.radius);
		}
	}
}
