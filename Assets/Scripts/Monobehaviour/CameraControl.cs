﻿using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public float dragRate;
	public float zoomRate;
    [Header("x is lower limit, y is upper limit")]
    public Vector2 zoomLimit;

	private Rect dragLimits;

	private Vector2 difference;
	private Vector3 pos;
	private bool dragging = false;

	void Start()
	{
		dragLimits = GameController.control.level.levelArea;
	}

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
			difference = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		
		if (Input.GetMouseButtonUp(1))
		{
			dragging = false;
		}
		
		if (Input.GetMouseButton(1))
		{
			transform.position -=(Camera.main.ScreenToWorldPoint(Input.mousePosition))-(Vector3)difference+Vector3.forward*10;
	
			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x,dragLimits.x,dragLimits.xMax),
				Mathf.Clamp(transform.position.y,dragLimits.y,dragLimits.yMax),
				transform.position.z);
		} 
	}

	void Zoom()
	{
        if ((Camera.main.orthographicSize > zoomLimit.x && Input.GetAxis("Mouse ScrollWheel") > 0) ||
            (Camera.main.orthographicSize < zoomLimit.y && Input.GetAxis("Mouse ScrollWheel") < 0))
        {
            Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomRate;
        }
	}
}
