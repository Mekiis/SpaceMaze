using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class Player : MonoBehaviour {

	public Floor origin = null;
	public Floor destFloor = null;

	public float speed = 1f;
	[Range(0.01f, 1)]
	public float distanceThreshold = 0.1f;

	public bool canMove = true;

	private List<ATile> path = null;
	private Vector3 dest;

	// Use this for initialization
	void Start () 
	{
		//this.transform.position = new Vector3 (origin..transform.position.x, this.transform.position.y, origin.transform.position.z);
		GetPath (origin, destFloor);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(path != null && canMove)
		{
			float x = Mathf.Lerp(this.transform.position.x, dest.x, speed * Time.deltaTime);
			float z = Mathf.Lerp(this.transform.position.z, dest.z, speed * Time.deltaTime);
			this.transform.position = new Vector3(x, this.transform.position.y, z);

			float distance = Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2(dest.x, dest.z));
			if( distance > -0.1 && distance < 0.1)
				canMove = false;
		}
	}

	void GetPath(Floor a_origin, Floor a_dest)
	{
		path = FloorManager.Instance.GetPath (a_origin, a_dest);
		/*
		if (path.Count > 0)
			dest = path [1].transform.position;
		else
			dest = this.transform.position;

		canMove = true;*/
	}
}
