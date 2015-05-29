using UnityEngine;
using System.Collections;

internal class SGMStatsManager
{
	internal void AddValueForStat(string a_userId, string a_key, float a_value = 1f, bool a_notify = true) {
		// Get user by ID
		SGMUser user = SGMGameManager.Instance.userManager.GetUserById(a_userId);
		if(user == null)
			return;

		float existingValue = 0f;
		user.stats.TryGetValue(a_key, out existingValue);

		SetValueForStat(a_userId, a_key, existingValue + a_value, a_notify);
	}

	internal void SetValueForStat(string a_userId, string a_key, float a_value, bool a_notify = true) {
		// Get user by ID
		SGMUser user = SGMGameManager.Instance.userManager.GetUserById(a_userId);
		if(user == null)
			return;

		// Set the stat
		user.stats.Add(a_key, a_value);

		// Notify manager
		if(a_notify)
		{
			SGMGameManager.Instance.unlockManager.MajUnlockForData(a_key, a_userId);
		}
	}
}
