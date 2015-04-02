using UnityEngine;
using System.Collections;

//public sealed class GizmoHandle : MonoBehaviour
//{
//	public GameObject gizmoParent; //The object to transform
//
//	public GameObject gizmoPoint; //The object to gizmo
//	public float gizmoSensitivity = 2.0f;
//	public bool needUpdate = false;
//
//	public enum GizmoType 
//	{
//		Position,
//		Scale
//	}
//
//	public GizmoType type = GizmoType.Position;
//	
//	private bool holding = false;
//	
//	public void SetType(GizmoType type)
//	{
//		this.type = type;
//		posEnd.SetActive	(type == GizmoType.Position);
//		scaleEnd.SetActive	(type == GizmoType.Scale);
//	}
//	
//	public void OnPress()
//	{
//		holding = true;
//	}
//
//	public void OnUnpress() 
//	{
//		holding = false;
//		needUpdate = true;
//	}
//	
//	
//	public void OnDrag() 
//	{
//		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		mousePos.z = 0;
//		if (holding) 
//		{
//			switch (type)
//			{
//			case GizmoType.Position:
//				gizmoParent.transform.position = mousePos;
//				break;
//			case GizmoType.Scale:
//				otherTrans.localScale *= mousePos.magnitude * gizmoSensitivity;
//				break;
//			}
//		}
//	}
//	
//	
//
//}
//
//public sealed class Gizmo
//{
//
//	public GizmoHandle transformGizmo;
//	public GizmoHandle.GizmoType gizmoType;
//
//	var needUpdate: boolean = false;
//	
//	function Awake() {
//		axisX.axis = GizmoAxis.X;
//		axisY.axis = GizmoAxis.Y;
//		axisZ.axis = GizmoAxis.Z;
//		
//		setType(gizmoType);
//	}
//	
//	
//	function Update () {    
//		needUpdate = (axisX.needUpdate || axisY.needUpdate || axisZ.needUpdate);
//	}
//	
//	function setType(type: GizmoType) {
//		axisX.setType(type);
//	}
//	
//	function setParent(other: Transform) {
//		transform.parent = other;
//		axisX.setParent(other);
//		axisY.setParent(other);
//		axisZ.setParent(other);
//	}
//}
//
//public class GameObjectGizmo : MonoBehaviour
//{
//	public 	GameObject 	gizmoAxis;
//	public 	float 		gizmoSize = 1.0f;
//
//	private	GameObject	gizmoObj;
//	private Gizmo		gizmo;
//	private GizmoType	gizmoType = GizmoType.Position;
//
//	void Update()
//	{
//		if (Input.GetKeyDown( KeyCode.Escape ))
//		{
//			removeGizmo();
//		}
//		
//		if (gizmo)
//		{
//			if (Input.GetKeyDown(KeyCode.Alpha1)) 
//			{
//				gizmoType = GizmoType.Position;
//				gizmo.setType(gizmoType);
//			}
//			if (Input.GetKeyDown(KeyCode.Alpha2))
//			{
//				gizmoType = GizmoType.Scale;
//				gizmo.setType(gizmoType);
//			}
//			if (gizmo.needUpdate) 
//			{
//				resetGizmo();
//			}
//		}
//}
//
//
//
//
//function OnMouseDown() {
//	if (!gizmoObj) {
//		resetGizmo();
//	}
//}
//
//function removeGizmo() {
//	if (gizmoObj) {
//		gameObject.layer = 0;
//		for (var child : Transform in transform) {
//			child.gameObject.layer = 0;
//		}        
//		Destroy(gizmoObj);    
//		Destroy(gizmo);    
//	}
//}
//
//function resetGizmo() {
//	removeGizmo();
//	gameObject.layer = 2;
//	for (var child : Transform in transform) {
//		child.gameObject.layer = 2;
//	}        
//	gizmoObj = Instantiate(gizmoAxis, transform.position, transform.rotation);
//	gizmoObj.transform.localScale *= gizmoSize;
//	gizmo = gizmoObj.GetComponent("Gizmo");
//	gizmo.setParent(transform);
//	gizmo.setType(gizmoType);
//}

