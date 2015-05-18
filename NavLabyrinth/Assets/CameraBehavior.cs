using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {
	public Transform target;//the target object
	public float speedMod = 2.0f;//a speed modifier
	private Vector3 point;//the coord to the point where the camera looks at

	private bool isFirstInput = true;
	private Vector3 lastInput;
	private bool isDown = false;
	
	void Start () {//Set up things on the start method
		point = target.position;//get target's coords
		transform.LookAt(point);//makes the camera look to it
	}
	
	void Update () {//makes the camera rotate around "point" coords, rotating around its Y axis, 20 degrees per second times the speed modifier
		if(Input.GetMouseButtonDown(0))
		{
			isDown = true;

		}
		if(Input.GetMouseButtonUp(0) && !isFirstInput)
		{
			isDown = false;
			isFirstInput = true;
		}

		if(isDown)
		{
			if(isFirstInput)
			{
				lastInput = Input.mousePosition;
				isFirstInput = false;
			}
			
			Vector3 diff = lastInput - Input.mousePosition;
			Debug.Log (lastInput+" "+Input.mousePosition);

			transform.LookAt(target);
			//transform.Translate(Vector3.right * diff.x * speedMod * Time.deltaTime);
			//transform.Translate(Vector3.up * diff.y * speedMod * Time.deltaTime);
			
			transform.RotateAround (point, Vector3.right, diff.y  * speedMod);
			transform.RotateAround (point, Vector3.up, -diff.x * speedMod);
			
			lastInput = Input.mousePosition;
		}

	}
}