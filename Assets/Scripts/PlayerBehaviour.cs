using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
	public float gravityConstant = 1.0f;
	public float timeDistortionConstant = 1.0f;
	public Vector2 initialSpeed = new Vector2();

	GameObject[] attractors;
	float _time = 0.0f;
	public float time {get{return _time;}}

	void Start ()
	{
		GetComponent<Rigidbody2D>().AddForce(initialSpeed * GetComponent<Rigidbody2D>().mass * 60.0f);
		attractors = GameObject.FindGameObjectsWithTag("Attractor");
	}

	void Update ()
	{
		DoWorldTime();
		DoLocalTime ();
	}

	void FixedUpdate ()
	{
		DoGravity();
	}



	
	void DoWorldTime ()
	{
		float t = Time.deltaTime * (1 + GetGravityForce().magnitude * timeDistortionConstant);
		World.AddTime(t);
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
