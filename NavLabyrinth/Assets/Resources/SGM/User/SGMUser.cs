using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class SGMUser
{
	internal string id = "";
	internal Dictionary<string, float> stats = new Dictionary<string, float>();

	public SGMUser(string a_id)
	{
		id = a_id;

		SGMGameManager.Instance.userManager.RegisterUser(this);
	}
	
	internal void Save()
	{
		
	}
	
	protected virtual void _Save()
	{
		
	}
	
	internal void Load()
	{
		
	}
	
	protected virtual void _Load()
	{
		
	}
}
