using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour, IGameEditable
{
	public GameEditableValues Values { get; set; }

	public Vector2 initialSpeed = new Vector2();
	public Vector2 localVelocity = Vector2.zero;

	float _time = 0.0f;
	public float time {get{return _time;} set{_time = value;}}

	Vector2 timeDilationForce;
	[Range(0.1f,100.0f)]
	public float localTimeDilationFactor;
	[Range(0.1f,100.0f)]
	public float relativeTimeDilationFactor;

	void Start ()
	{
		WorldScene.Instance.dataEntries.Add(this);
		localVelocity = initialSpeed;
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
		localVelocity		 	= Values.velocity;
		_time = 0.0f;
	}

	void Update ()
	{
		DoLocalTime ();
		DoTimeDilation();
		MoveShip();
	}

	void DoLocalTime()
	{
		if (GameController.mode == GameController.Mode.PLAY)
		{
			_time += Time.deltaTime * relativeTimeDilationFactor;
		}
	}
	
	void DoTimeDilation()
	{
		UpdateTimeDilationFactor();
		ResetTimeDilationForce();
	}

	void UpdateTimeDilationFactor()
	{
		UpdateLocalTimeDilationFactor();
		if (GameController.control.currentShip == this)
		{
			UpdateWorldRelativeTimeDilationFactor();
		}
		UpdateRelativeTimeDilationFactor();
	}

	void UpdateLocalTimeDilationFactor()
	{
		localTimeDilationFactor = 1 / (1 + timeDilationForce.magnitude * World.timeDistortionConstant);
	}
	
	public void UpdateWorldRelativeTimeDilationFactor ()
	{
		World.relativeTimeDilationFactor = 1/localTimeDilationFactor;
    }
	
	void UpdateRelativeTimeDilationFactor()
	{
		relativeTimeDilationFactor = localTimeDilationFactor * World.relativeTimeDilationFactor;
	}

	void ResetTimeDilationForce ()
	{
		timeDilationForce = Vector2.zero;
	}

	public void AddTimeDilationForce (Vector2 force)
	{
		timeDilationForce += force;
	}

	public void AddForce(Vector2 force)
	{
		localVelocity += force * relativeTimeDilationFactor * Time.deltaTime;
	}

	void MoveShip ()
	{
		transform.position += (Vector3)localVelocity * relativeTimeDilationFactor * Time.deltaTime;
	}

	void OnDrawGizmosSelected()
	{
		Vector2 force = initialSpeed;
		Vector3 lastPos = transform.position;
		Vector3 pos = transform.position;

		int precision = 100; //higher values for more precise estimations
		int estimateTime = 10; //seconds to estimate ships orbital track
		for(int i = 0; i < (estimateTime*precision); i++)
		{
			force += BlackHoleBehaviour.GetPotentialGravityForce(gameObject, lastPos);
			pos += (Vector3)force * (1.0f/precision); //TODO a value must also represent relativeTimeDilation, it must be calculated each iteration
			Gizmos.DrawLine(lastPos, pos);
			lastPos=pos;
		}
	}
}
