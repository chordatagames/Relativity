using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour, IGameEditable
{
	public GameEditableValues Values { get; set; }

	public float gravityConstant = 1.0f;
	public float timeDistortionConstant = 8.0f;
	public Vector2 initialSpeed = new Vector2();

	float _time = 0.0f;
	public float time {get{return _time;} set{_time = value;}}

	private Rigidbody2D rigidbody;

	Vector2 timeDilationForce;

	void Start ()
	{
		WorldScene.Instance.dataEntries.Add(this);
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.AddForce(initialSpeed * GetComponent<Rigidbody2D>().mass * 60.0f); //TODO should really be called when the play-button ingame is pressed.
	}

	public void SetValues()
	{
		Values = new GameEditableValues(gameObject);
	}

	public void ResetValues()
	{
		transform.position 		= Values.position;
		transform.rotation		= Quaternion.Euler(Values.rotation);
		transform.localScale 	= Values.scale;
		rigidbody.velocity 		= Values.velocity;
		_time = 0.0f;
	}

	void Update ()
	{
		UpdateWorldTimeDilationFactor();
		DoLocalTime ();
	}

	void FixedUpdate ()
	{
		timeDilationForce = Vector2.zero;
	}

	//
	// Methods called from above
	//
	void UpdateWorldTimeDilationFactor ()
	{
		World.timeDilationFactor = 1 + timeDilationForce.magnitude * timeDistortionConstant;
	}

	public void addTimeDilationForce (Vector2 force)
	{
		timeDilationForce += force;
	}

	void DoLocalTime()
	{
		if (GameController.mode == GameController.Mode.PLAY)
		{
			_time += Time.deltaTime;
		}
	}

	void DoGravity ()
	{
		GetComponent<Rigidbody2D>().AddForce(GetGravityForce());
	}

	Vector2 GetGravityForce ()
	{
		Vector2 force = new Vector2 ();

		foreach (GameObject attractor in GameObject.FindGameObjectsWithTag("Attractor"))
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
