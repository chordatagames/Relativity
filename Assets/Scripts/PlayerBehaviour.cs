using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
	public float gravityConstant = 1.0f;
	public Vector2 initialSpeed = new Vector2();

	GameObject[] attractors;

	void Start ()
	{
		GetComponent<Rigidbody2D>().AddForce(initialSpeed * GetComponent<Rigidbody2D>().mass * 60.0f);
		attractors = GameObject.FindGameObjectsWithTag("Attractor");
	}

	void FixedUpdate ()
	{
		DoGravity();
	}

	void DoGravity ()
	{
		GetComponent<Rigidbody2D>().AddForce(GetGravityForce());
	}

	Vector2 GetGravityForce ()
	{
		Vector2 force = new Vector2 ();
		foreach (GameObject attractor in attractors)
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
