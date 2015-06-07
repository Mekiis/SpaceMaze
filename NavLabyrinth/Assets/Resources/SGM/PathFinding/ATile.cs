using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
internal class Link 
{
	public ATile Tile = null;
	public float Weight = 1f;
}

internal abstract class ATile : MonoBehaviour
{
	#region Inspector Properties
	public float weight 				= 1f;
	public bool isWalkable 				= true;
	#endregion

	public abstract List<Link> GetConnections();
}
