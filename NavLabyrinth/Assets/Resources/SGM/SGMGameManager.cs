using UnityEngine;
using System.Collections;

internal class SGMGameManager : MonoBehaviour
{
	#region Inspector Properties
	public AGameManager gameManager = null;
	#endregion

	#region Internal Properties
	internal SGMTimeManager timeManager = null;
	internal SGMUserManager userManager = null;
	internal SGMUnlockManager unlockManager = null;
	#endregion
	
	#region Instance
	internal static SGMGameManager Instance;

	void Awake()
	{
		if(Instance != null)
			GameObject.Destroy(Instance);
		else
			Instance = this;
		
		DontDestroyOnLoad(this);

		timeManager = new SGMTimeManager();
		userManager = new SGMUserManager();
		unlockManager = new SGMUnlockManager();
	}
	#endregion
}

