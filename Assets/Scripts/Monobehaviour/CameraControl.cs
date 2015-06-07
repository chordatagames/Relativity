using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public float dragRate;
	public float zoomRate;

	private Vector2 difference;
	private Vector2 pos;
	private bool dragging = false;

	void LateUpdate() 
	{
		// they're placed in their own functions because they're completely
		// unrelated, not because they're meant to be reused. zoom is too
		// simple to place in its own class.
		Drag();
		Zoom();
	}
	
	void Drag()
	{
		// camera dragging: this is the kinda stuff that tends to be unrebindable,
		// so i don't see a big problem with just hard coding it like this.
		if (Input.GetMouseButtonDown(1))
		{
			dragging = true;
			difference = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			pos = transform.position;
		}
		
		if (Input.GetMouseButtonUp(1))
		{
			dragging = false;
		}
		
		if (Input.GetMouseButton(1))
		{
			transform.position = new Vector3
			(
				pos.x - (Camera.main.ScreenToViewportPoint(Input.mousePosition).x-difference.x) * dragRate,
				pos.y - (Camera.main.ScreenToViewportPoint(Input.mousePosition).y-difference.y) * dragRate,
				transform.position.z
			);
		} 
	}

	void Zoom()
	{
		// same as the above w/r/t hardcoded controls.
		Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomRate;
	}
}
