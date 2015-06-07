using UnityEngine;
using System.Collections;

public sealed class GizmoHandle : MonoBehaviour
{

	public Transform gizmoParent; //The object to transform

	public float gizmoSensitivity = 2.0f;

	[HideInInspector()]
	public bool needUpdate = false;

	private bool holding = false;
	private Vector3	scaleCenter;

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
			switch (GameController.mode)
			{
			case GameController.Mode.POSITIONING:
				gizmoParent.transform.position = Grid.Gridify(mousePos);
				break;
			case GameController.Mode.SCALING:
				gizmoParent.localScale = Grid.Gridify(Vector3.one + Vector3.one * (mousePos - scaleCenter).sqrMagnitude * gizmoSensitivity);
				gizmoParent.GetComponent<BlackHoleBehaviour>().mass = gizmoParent.localScale.sqrMagnitude;
				break;
			default:
				break;
			}
		}
	}
}