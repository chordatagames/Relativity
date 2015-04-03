using UnityEngine;
using System.Collections;

public class BlackHoleBehaviour : MonoBehaviour
{
	public float mass;
	public float rotationSpeed;

	void Start()
	{
		WorldScene.AddAttractor(gameObject);
	}

	void Update ()
	{
		Angles.RotateTransform(transform, Time.deltaTime * World.timeDilationFactor * rotationSpeed);
	}
}
