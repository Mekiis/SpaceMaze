using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

internal enum EElement{
	Water = 0,
	Fire,
	Wind,
	Earth
}

[Serializable]
internal class Resistance 
{
	public EElement Element = EElement.Water;
	public float Value = 0f;
}

internal class UnitManager : MonoBehaviour
{
	#region Public Properties
	public List<Unit> unitPrototypes = new List<Unit>();
	#endregion

	#region Private Properties
	private List<Unit> _units = new List<Unit>();
	#endregion

	public void InstantiateUnit(string a_unitId, int a_number, Path a_path)
	{
		foreach(Unit unit in unitPrototypes)
		{
			if(unit.id.ToLower().CompareTo(a_unitId.ToLower()) == 0)
			{
				for(int i = 0; i < a_number; i++)
				{
					Unit unitInstantiate = Instantiate(unit, Vector3.zero, Quaternion.identity) as Unit;
					unitInstantiate.gameObject.SetActive(true);
					unitInstantiate.transform.position = a_path.spawnTiles.transform.position + a_path.spawnTiles.transform.up * unit.height;
					unitInstantiate.PathData = a_path;
					unitInstantiate.Interuptor(true);
					_units.Add(unitInstantiate);
				}
				return;
			}
		}
	}

	public void InstantiateUnit(string a_unitId) {
		InstantiateUnit(a_unitId, 1, (SGMGameManager.Instance.gameManager as GameManager).fieldManager.paths[UnityEngine.Random.Range(0, (SGMGameManager.Instance.gameManager as GameManager).fieldManager.paths.Count)]); 
	}
}

