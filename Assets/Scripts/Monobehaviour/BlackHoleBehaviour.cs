using UnityEngine;
using System.Collections;
using System;

public class BlackHoleBehaviour : GameObstacle
{
	public float mass;
	public float rotationSpeed;
	public float areaOfEffectRadius;

	CircleCollider2D areaOfEffect;
	Transform areaOfEffectUI;

	void Start()
	{
		areaOfEffect = GetComponent<CircleCollider2D>();
		areaOfEffectUI = transform.FindChild("Canvas").FindChild("Area of Effect");
		UpdateAreaOfEffectSize();
		WorldScene.Instance.dataEntries.Add(this);
	}

	void Update ()
	{
		Angles.RotateTransform(transform, Time.deltaTime * World.timeDilationFactor * rotationSpeed);
		UpdateAreaOfEffectSize ();
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Ship")
		{
			Rigidbody2D colrig = col.transform.GetComponent<Rigidbody2D> ();
			Vector2 force = (Vector2)(transform.position - col.transform.position).normalized
				* col.transform.GetComponent<PlayerBehaviour> ().gravityConstant
				* mass
				* colrig.mass
				* Mathf.Pow ((transform.position - col.transform.position).magnitude, -2);
			colrig.AddForce(force);
			col.transform.GetComponent<PlayerBehaviour>().addTimeDilationForce(force);
		}
	}

	void UpdateAreaOfEffectSize()
	{
		areaOfEffect.radius = areaOfEffectRadius;
		areaOfEffectUI.localScale = new Vector3 (areaOfEffectRadius * 2, areaOfEffectRadius * 2, 0);
	}

	public override LevelObject[] GetValues()
	{
		
	}

	public override void SetValues(ValueType[] vals)
	{
		foreach (ValueType val in vals) 
        {
			switch (val.key)
			{
				case "mass":
					mass = float.Parse(val.value[0]);
					break;
				case "position":
					transform.position = new Vector2(float.Parse(val.value[0]), float.Parse(val.value[1]));
					break;
			}
		}
	}

	public override void ResetValues()
	{
		throw new NotImplementedException();
	}
}