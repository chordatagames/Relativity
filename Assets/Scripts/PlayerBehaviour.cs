﻿using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
	public float gravityConstant = 1.0f;
	public float timeDistortionConstant = 8.0f;
	public Vector2 initialSpeed = new Vector2();

	float _time = 0.0f;
	public float time {get{return _time;}}

	void Start ()
	{
		WorldScene.AddShip(gameObject);
		GetComponent<Rigidbody2D>().AddForce(initialSpeed * GetComponent<Rigidbody2D>().mass * 60.0f);

	}

	void Update ()
	{
		UpdateWorldTimeDilationFactor();
		DoLocalTime ();
	}

	void FixedUpdate ()
	{
		DoGravity();
	}



	//
	// Methods called from above
	//
	void UpdateWorldTimeDilationFactor ()
	{
		World.timeDilationFactor = 1 + GetGravityForce().magnitude * timeDistortionConstant;
	}

	void DoLocalTime()
	{
		_time += Time.deltaTime;
	}

	void DoGravity ()
	{
		GetComponent<Rigidbody2D>().AddForce(GetGravityForce());
	}

	Vector2 GetGravityForce ()
	{
		Vector2 force = new Vector2 ();
		foreach (GameObject attractor in WorldScene.Attractors)
		{
			force	+= (Vector2)(attractor.transform.position - transform.position).normalized
					* gravityConstant
					*  attractor.GetComponent<BlackHoleBehaviour>().mass
					*  GetComponent<Rigidbody2D>().mass
					* Mathf.Pow((transform.position - attractor.transform.position).magnitude, -2);
		}
		return force;
	}
}
