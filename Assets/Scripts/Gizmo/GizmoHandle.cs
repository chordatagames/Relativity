using UnityEngine;
using System.Collections;

public sealed class GizmoHandle : MonoBehaviour
{
	public Transform gizmoParent; //The object to transform

	public float gizmoSensitivity = 2.0f;
	[HideInInspector()]
	public bool needUpdate = false;
	[HideInInspector()]
	public enum GizmoType 
	{
		Position,
		Scale
	}

	public 	GizmoType 	type 	= GizmoType.Position;
	private bool 		holding = false;
	private Vector3		scaleCenter;




	public void SetType(GizmoType type)
	{
		this.type = type;
	}
	
	public void OnPress()
	{
		scaleCenter = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		scaleCenter.z = 0;
		holding = true;
	}

	public void OnUnpress() 
	{
		holding = false;
		needUpdate = true;
	}
	
	void Update() 
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;
		if (holding) 
		{
			switch (type)
			{
			case GizmoType.Position:
				gizmoParent.transform.position = Grid.Gridify(mousePos);
				break;
			case GizmoType.Scale:
				gizmoParent.localScale = Grid.Gridify(Vector3.one + Vector3.one * (mousePos - scaleCenter).sqrMagnitude * gizmoSensitivity);
				break;
			}
		}
	}
}