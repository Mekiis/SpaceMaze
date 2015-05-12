using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal abstract class ATile : MonoBehaviour 
{
	#region Inspector Properties
	public float weight 				= 1f;
	public bool isBlocked 				= false;

	public List<ATile> connection 		= new List<ATile>();
	#endregion
}
