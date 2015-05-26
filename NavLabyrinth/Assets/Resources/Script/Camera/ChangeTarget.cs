using UnityEngine;
using System.Collections;

public class ChangeTarget : MonoBehaviour
{
	public CameraBehavior cameraBehavior = null;

	[Range(0.0001f, 2f)]
	public float movementDetectionThreshold = 0.1f;

	private Vector2 movement = new Vector2(0f, 0f);

	void Update()
	{
		int layerMask = 1 << 8;


		if(Input.GetMouseButton(0))
		{
			movement.x += Mathf.Abs(Input.GetAxis("Mouse X"));
			movement.y += Mathf.Abs(Input.GetAxis("Mouse Y"));
		}
		if(Input.GetMouseButtonUp(0)) 
		{
			if(movement.x < movementDetectionThreshold && movement.y < movementDetectionThreshold)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
				{
					Planete p = hit.collider.GetComponentInParent<Planete>();
					if(p != null && cameraBehavior != null)
					{
						cameraBehavior.Target = p.center;
						cameraBehavior.zoomMinLimit = p.GetRange();
					}
				}
			}

			movement = new Vector2(0f, 0f);
		}
	}
}

