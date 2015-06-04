using UnityEngine;
using System.Collections;

internal class Missile : MonoBehaviour {
	[Tooltip("Unity unit")]
	public float range = 10f;
	[Tooltip("Meter / Second")]
	public float speed = 1f;
	[Tooltip("Dammage deal")]
	public float dammage = 1f;

	private Vector3 _destination = new Vector3(0, 0, 0);

	private bool _move = false;

	public void StartMissile(Vector3 a_start, Vector3 a_destination)
	{
		transform.position = a_start;
		_destination = a_destination;
		
		_move = true;
	}

	void Update()
	{
		if(_move)
		{
			Vector3 displacement = _destination - transform.position;
			displacement.Normalize();
			displacement *= speed * SGMGameManager.Instance.timeManager.DeltaTime(ETimeCategory.Gameplay);
			
			float distanceBefore = Vector3.Distance(_destination, transform.position);
			transform.position += displacement;
			float distanceAfter = Vector3.Distance(_destination, transform.position);
			
			if(distanceAfter > distanceBefore)
			{
				_move = false;
				Explode();
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<Unit>() != null)
		{
			other.GetComponent<Unit>().OnHit(dammage);
			Explode();
		}
	}

	protected virtual void Explode()
	{
		// TODO : Add explosion particules
	}

	protected virtual void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		
		SphereCollider collider = GetComponent<SphereCollider>();
		if(collider != null)
		{
			collider.radius = range;
			collider.isTrigger = true;
			Gizmos.DrawWireSphere(this.transform.position + collider.center, collider.radius);
		}
	}
}
