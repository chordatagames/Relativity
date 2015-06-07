using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour {
	public float dragRate;

	private Vector2 difference;
	private Vector2 pos;
	private bool dragging = false;

	void LateUpdate() {
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
}
