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
		// TODO : Find best accecible target

		if(_target != null)
		{
			// TODO : Instantiate bullet on target !
			_timeBeforeNextShot = 1f/speed;
		}
	}
}
