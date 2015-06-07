using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class Tower : MonoBehaviour {
	#region Inspector Properties
	public string id = "";
	public string displaybleName = "";
	[Tooltip("Unity unit")]
	public float range = 10f;
	[Tooltip("Number of attack / Second")]
	public float speed = 1f;
	[Tooltip("Dammage deal")]
	public float dammage = 1f;
	public List<Tower> parents = new List<Tower>();
	public Bullet bullet = null;
	public Transform launchPoint = null;
	#endregion

	internal List<Unit> targets = new List<Unit>();

	private Unit _target = null;
	private float _timeBeforeNextShot = 0f;

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

	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<Unit>() != null)
			targets.Add(other.GetComponent<Unit>());
	}

	void OnTriggerExit(Collider other) {
		if(other.GetComponent<Unit>() != null)
			targets.Remove(other.GetComponent<Unit>());
	}

	void Start()
	{
		ResetScript();
	}

	internal void ResetScript()
	{
		targets = new List<Unit>();
		_timeBeforeNextShot = 1f/speed;
	}

	void Update()
	{
		_timeBeforeNextShot -= SGMGameManager.Instance.timeManager.DeltaTime(ETimeCategory.Gameplay);
		if(_timeBeforeNextShot < 0f)
		{
			_timeBeforeNextShot = 0f;
			LaunchShot();
		}
	}

	private void LaunchShot()
	{
		int layerMask = 1 << 8;

		float minDistance = Mathf.Infinity;
		targets.RemoveAll(p=>p == null);
		foreach(Unit unit in targets)
		{
			Ray ray = new Ray (launchPoint.position, (unit.transform.position - launchPoint.position).normalized);
			RaycastHit hit;
			
			if(!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && Vector3.Distance(launchPoint.position, unit.transform.position) < minDistance) {
				minDistance = Vector3.Distance(launchPoint.position, unit.transform.position);
				_target = unit;
			}
		}


		if(_target != null)
		{
			Bullet missileInstantiate = Instantiate(bullet, Vector3.zero, Quaternion.identity) as Bullet;
			missileInstantiate.gameObject.SetActive(true);
			// TODO : Instantiate bullet on target !
			missileInstantiate.transform.rotation = launchPoint.rotation;
			missileInstantiate.StartMissile(launchPoint.position, _target.transform.position);

			_timeBeforeNextShot = 1f/speed;
		}
	}
}
