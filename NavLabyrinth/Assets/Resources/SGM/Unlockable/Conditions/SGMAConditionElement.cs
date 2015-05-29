using UnityEngine;
using System.Collections;

internal abstract class SGMAConditionElement
{
	internal abstract bool Compute(string a_userId);
	
	internal abstract float GetPercentCompletion(string a_userId);
}
