using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class Planete : MonoBehaviour
{
	public Transform center = null;
	public List<ATile> tiles = new List<ATile>();

	public void FillAttributsWithChidrens()
	{
		tiles.Clear();
		ATile[] list = GetComponentsInChildren<ATile>();
		foreach(ATile tile in list)
			tiles.Add(tile);

		foreach (Transform child in transform)
		{
			if(child.name.ToLower().CompareTo("center") == 0) center = child ;
		}
	}

	internal float GetRange()
	{
		float maxDistance = 0f;

		if(center != null)
		{
			foreach (ATile tile in tiles) 
			{
				float distance = Vector3.Distance(center.position, tile.transform.position);
				if(distance > maxDistance) maxDistance = distance;
			}
		}
		else
			Debug.LogError("You need a \"center\" for planete : "+this.name);

		return maxDistance + maxDistance;
	}

	protected virtual void OnDrawGizmosSelected () {
		Gizmos.color = Color.yellow;
		if(GetRange() > 0f)
			Gizmos.DrawWireSphere(center.position, GetRange());
	}
}

