using UnityEngine;
using System.Collections;

internal class SGMGameManager : MonoBehaviour
{
	internal SGMTimeManager timeManager = new SGMTimeManager();

	internal static SGMGameManager Instance;
	
	#region Instance
	void Awake()
	{
		if(Instance != null)
			GameObject.Destroy(Instance);
		else
			Instance = this;
		
		DontDestroyOnLoad(this);
	}
	#endregion
}

