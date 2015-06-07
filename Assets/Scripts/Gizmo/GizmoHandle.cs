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
		POSITION,
		SCALE,
		NONE
	}

	public static GizmoType type = GizmoType.POSITION;

	private bool holding = false;
	private Vector3	scaleCenter;
	private CanvasRenderer renderer;

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

	void Awake()
	{
		renderer = GetComponent<CanvasRenderer>();
	}

	void Update()
	{
		renderer.SetAlpha( (GameController.mode != GameController.Mode.PLAY) ? 1 : 0 );
		
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;
		if (holding && GameController.control.blackholeList.Contains(gizmoParent.gameObject)) 
		{
			switch (type)
			{
			case GizmoType.POSITION:
				gizmoParent.transform.position = Grid.Gridify(mousePos);
				break;
			case GizmoType.SCALE:
				gizmoParent.localScale = Grid.Gridify(Vector3.one + Vector3.one * (mousePos - scaleCenter).sqrMagnitude * gizmoSensitivity);
				gizmoParent.GetComponent<BlackHoleBehaviour>().mass = gizmoParent.localScale.sqrMagnitude;
				break;
			case GizmoType.NONE:
				break;
			}
		}
	}
}