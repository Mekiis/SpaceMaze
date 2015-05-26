using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour
{
	public Transform Target = null;

	public Vector3 CameraOffset = new Vector3(0,0,10);
	public int CameraSpeed = 200;

	public int yMinLimit = -90;
	public int yMaxLimit = 90;

	public float zoomMinLimit = 0f;

	public float changeTargetSpeed = 1f;

	private Vector2 Position2D;
	private Vector3 TargetPositionSmooth;

	void Start () 
	{
		TargetPositionSmooth = Target.position;
		this.transform.eulerAngles = new Vector3(0, 0, 0);
		this.transform.position = new Vector3(Target.position.x, Target.position.y + CameraOffset.y, Target.position.z - CameraOffset.z);
	}

	void LateUpdate()
	{
		UpdateCamera();
	}

	void UpdateCamera () {
		TargetPositionSmooth = Lerp(TargetPositionSmooth, Target.position, changeTargetSpeed * Time.deltaTime);
		CameraOffset.z = Mathf.Clamp(CameraOffset.z, zoomMinLimit, float.MaxValue);
		Quaternion rot = Quaternion.Euler(Position2D.y, Position2D.x, 0);
		Vector3 pos = rot * new Vector3(0, CameraOffset.y, -CameraOffset.z) + TargetPositionSmooth;

		// Mouse
		if(Input.GetMouseButton(0)) {
			Position2D.x += Input.GetAxis("Mouse X") * CameraSpeed * Time.deltaTime;
			Position2D.y -= Input.GetAxis("Mouse Y") * CameraSpeed * Time.deltaTime;
		}
		CameraOffset.z -= Input.GetAxis("Mouse Zoom") * CameraSpeed * Time.deltaTime;

		// Keyboard + Joystick
		Position2D.x += -Input.GetAxis("Horizontal") * CameraSpeed * Time.deltaTime;
		Position2D.y -= -Input.GetAxis("Vertical") * CameraSpeed * Time.deltaTime;
		CameraOffset.z -= Input.GetAxis("Zoom") * CameraSpeed * Time.deltaTime;

		Position2D.y = ClampAngle(Position2D.y, yMinLimit, yMaxLimit);


		this.transform.position = pos;
		this.transform.rotation = rot;
	}

	public Vector3 Lerp(Vector3 from, Vector3 to, float t)
	{
		Vector3 res = new Vector3(0, 0, 0);

		res.x = Mathf.Lerp(from.x, to.x, t);
		res.y = Mathf.Lerp(from.y, to.y, t);
		res.z = Mathf.Lerp(from.z, to.z, t);

		return res;
	}

	public float ClampAngle (float angle, float min, float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		
		return Mathf.Clamp (angle, min, max);
	}
}

