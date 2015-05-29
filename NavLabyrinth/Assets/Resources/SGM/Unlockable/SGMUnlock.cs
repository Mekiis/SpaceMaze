using UnityEngine;
using System.Collections;

internal class SGMUnlock
{
	internal string id = "";

	private SGMAConditionElement _conditions;
	
	public SGMUnlock(string a_id, SGMAConditionElement a_conditions){
		id = a_id;
		_conditions = a_conditions;
	}
	
	internal bool IsUnlocked(string a_userId){
		return (_conditions == null ? true : _conditions.Compute(a_userId));
	}
	
	internal float GetCompletionPercent(string a_userId){
		return (_conditions == null ? 100f : _conditions.GetPercentCompletion(a_userId));
	}
}

