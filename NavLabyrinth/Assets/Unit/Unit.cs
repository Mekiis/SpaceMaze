using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
internal enum EUnitState{
	Stop = 0,
	Move,
	SearchPath
}

internal class Unit : MonoBehaviour
{
	#region Public Properties
	public string id = "default";
	public float lifePoint = 1f;
	public float speed = 1f;
	public List<Resistance> resistances = new List<Resistance>();
	
	public float height = 1f;
	#endregion

	#region Private Properties
	private Path _pathData = null;
	internal Path PathData 
	{
		get { return _pathData; }
		set
		{
			_pathData = value;
			state = EUnitState.Stop;
		}
	}
	private List<ATile> pathToFollow = new List<ATile>();

	private int currentTileId = -1;
	private ATile nextTile = null;
	private GameObject currentGameObject = null;
	private ATile currentTile = null;

	private EUnitState state = EUnitState.Stop;
	#endregion

	void Awake()
	{
		state = EUnitState.Stop;
	}

	internal void Interuptor(bool a_state)
	{
		if(!a_state)
			state = EUnitState.Stop;
		if(a_state)
			state = EUnitState.SearchPath;
	}

	void Update () 
	{
		switch (state)
		{
		case EUnitState.Stop:
			break;
		case EUnitState.Move:
			MajActualFloor();
			currentTileId = MajCurrentTileId(currentTileId, currentTile);
			nextTile = GetNexTile(currentTileId);
			if(nextTile != null)
				MoveToTile(nextTile);
			break;
		case EUnitState.SearchPath:
			MajActualFloor();
			if(PathData != null && currentTile != null)
			{
				pathToFollow = FloorManager.Instance.GetPath(currentTile, PathData.destinationTile);
				SetOnTopOfObject(currentTile.transform);
				state = EUnitState.Move;
			}
			else
				Debug.Log ("No path data or currentTile for this Unit : "+this.name);
			break;
		}
	}

	private void MajActualFloor()
	{
		int layerMask = 1 << 8;

		// Physics
		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(transform.position, transform.TransformDirection (Vector3.down), out hit, Mathf.Infinity, layerMask)) 
		{
			//SetOnTopOfObject(hit.collider.gameObject.transform);
			if(currentGameObject != hit.collider.gameObject)
			{
				currentGameObject = hit.collider.gameObject;
				ATile tile = hit.collider.gameObject.GetComponent<ATile>();
				if(tile && tile != currentTile)
					currentTile = tile;
				Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.down) * hit.distance, Color.yellow);
			}
		} 
		else 
		{
			Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.down) *1000, Color.white);
		}
	}

	private void MoveToTile(ATile a_tileDestination)
	{
		float x = Corp(this.transform.position.x, a_tileDestination.transform.position.x + (height * a_tileDestination.transform.up.x), speed * SGMGameManager.Instance.timeManager.DeltaTime(ETimeCategory.Gameplay));
		float y = Corp(this.transform.position.y, a_tileDestination.transform.position.y + (height * a_tileDestination.transform.up.y), speed * SGMGameManager.Instance.timeManager.DeltaTime(ETimeCategory.Gameplay));
		float z = Corp(this.transform.position.z, a_tileDestination.transform.position.z + (height * a_tileDestination.transform.up.z), speed * SGMGameManager.Instance.timeManager.DeltaTime(ETimeCategory.Gameplay));

		this.transform.position = new Vector3(x, y, z);

		/*
		float distance = Vector3.Distance(this.transform.position, new Vector3(
		a_tileDestination.transform.position.x + (height * a_tileDestination.transform.up.x),
		a_tileDestination.transform.position.y + (height * a_tileDestination.transform.up.y),
		a_tileDestination.transform.position.z + (height * a_tileDestination.transform.up.z)
		));
		if( distance > -0.1 && distance < 0.1)
			canMove = false;
			*/
	}

	private int MajCurrentTileId(int a_currentTileId, ATile a_currentTile)
	{
		int currentTileId = -1;

		if(a_currentTileId == -1 || !a_currentTile.Equals(pathToFollow[a_currentTileId]))
		{
			currentTileId = -1;
			for(int i = 0; i < pathToFollow.Count; i++)
			{
				if(currentTile.Equals(pathToFollow[i]))
					currentTileId = i;
			}
		}
		else
		{
			currentTileId = a_currentTileId;
		}

		return currentTileId;
	}

	private ATile GetNexTile(int a_currentTileId)
	{
		ATile nextTile = null;
		
		if(a_currentTileId > -1 && pathToFollow != null)
		{
			int destinationTileId = currentTileId;
			if(currentTileId < pathToFollow.Count-1)
				destinationTileId++;

			nextTile =  pathToFollow[destinationTileId];
		}

		return nextTile;
	}

	internal void SetOnTopOfObject(Transform a_transform)
	{
		Vector3 v = new Vector3();
		Debug.Log (a_transform.up);
		v.x = this.transform.position.x * (1f - a_transform.up.x) + (a_transform.position.x + height) * a_transform.up.x;
		v.y = this.transform.position.y * (1f - a_transform.up.y) + (a_transform.position.y + height) * a_transform.up.y;
		v.z = this.transform.position.z * (1f - a_transform.up.z) + (a_transform.position.z + height) * a_transform.up.z;
		this.transform.position = v;

		CopyRotationOfObject(a_transform);
	}

	internal void CopyRotationOfObject(Transform a_transform)
	{
		this.transform.rotation = a_transform.rotation;
	}

	private float Corp(float a_from, float a_to, float a_t)
	{
		float res = a_from;

		if(a_from < a_to)
			res += a_t;
		else if(a_from > a_to)
			res -= a_t;

		if((a_from < a_to && res > a_to) || (a_from > a_to && res < a_to))
			res = a_to;

		return res;
	}

	internal void OnTeleport(ATile a_destinationToGo)
	{
		transform.position = a_destinationToGo.transform.position;
		SetOnTopOfObject(a_destinationToGo.transform);
	}

	internal void OnDestinationReach(ATile a_destinationReach)
	{
		if(a_destinationReach.Equals(PathData.destinationTile))
		{
			Destroy(this.gameObject);
		}
	}
}

