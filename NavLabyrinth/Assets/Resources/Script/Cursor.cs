using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	[Range(0.0001f, 2f)]
	public float movementDetectionThreshold = 0.1f;

	private AFloor _tileSelected = null;
	private Vector2 _mouseMovement = new Vector2(0f, 0f);

	// Use this for initialization
	void Awake () {
		_tileSelected = null;
	}
	
	// Update is called once per frame
	void Update () {
		int layerMask = 1 << 8;
		
		// Physics
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
		{
			Floor f = hit.collider.gameObject.GetComponent<Floor>();
			if(f == null)
				f = hit.collider.gameObject.GetComponentInParent<Floor>();

			if(f == null && _tileSelected != null)
			{
				_tileSelected.OnUnSelected();
				_tileSelected = f;
			}
			else if(f != null)
			{
				if(_tileSelected != f)
				{
					if(_tileSelected != null)
						_tileSelected.OnUnSelected();
					_tileSelected = f;
					_tileSelected.OnSelected();
				}
			}
		} 
		else 
		{
			if(_tileSelected != null)
			{
				_tileSelected.OnUnSelected();
				_tileSelected = null;
			}
		}

		if(Input.GetMouseButton(0))
		{
			_mouseMovement.x += Mathf.Abs(Input.GetAxis("Mouse X"));
			_mouseMovement.y += Mathf.Abs(Input.GetAxis("Mouse Y"));
		}
		if(Input.GetMouseButtonUp(0))
		{
			if(_mouseMovement.x < movementDetectionThreshold && _mouseMovement.y < movementDetectionThreshold)
			{
				if(_tileSelected != null)
					_tileSelected.OnClick();
			}
			
			_mouseMovement = new Vector2(0f, 0f);

		}
	}
}
