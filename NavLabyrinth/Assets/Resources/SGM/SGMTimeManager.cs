using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal enum ETimeCategory{
	Default = 0,
	UI,
	HUD,
	Gameplay
}

internal class SGMTimeManager
{
	#region Private Properties
	private Dictionary<ETimeCategory, float> timeModifiers = new Dictionary<ETimeCategory, float>();
	#endregion

	#region Time Management
	internal float DeltaTime(ETimeCategory a_category)
	{
		float timeModifier = 1f;
		if(timeModifiers.TryGetValue(a_category, out timeModifier))
			return Time.deltaTime * timeModifier;
		else
			return Time.deltaTime;
	}

	internal void SetTimeModifier(ETimeCategory a_category, float a_value)
	{
		timeModifiers[a_category] = a_value;
	}
	#endregion
}

