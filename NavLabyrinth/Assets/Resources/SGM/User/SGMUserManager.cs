using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class SGMUserManager
{
	private List<SGMUser> _users = new List<SGMUser>();

	internal SGMUser GetUserById(string a_id)
	{
		SGMUser userFind = null;

		foreach(SGMUser user in _users)
		{
			if(user.id.CompareTo(a_id) == 0)
				userFind = user;
		}

		return userFind;
	}

	internal bool RegisterUser(SGMUser userToAdd)
	{
		bool isAlreadyRgister = false;

		foreach(SGMUser user in _users)
		{
			if(user.id.CompareTo(userToAdd.id) == 0)
				isAlreadyRgister = true;
		}

		if(!isAlreadyRgister)
			_users.Add(userToAdd);

		return isAlreadyRgister;
	}
}
