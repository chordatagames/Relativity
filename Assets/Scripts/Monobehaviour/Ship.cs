using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	public LevelGoal[] shipGoals;
	public GameObject sprite;

	bool _completed;
	public bool completed { get { return _completed; } }

	private Rigidbody2D rigidbody;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		foreach (LevelGoal lg in shipGoals)
		{
			lg.tracking = gameObject;
		}
	}

	void Update() 
	{
		Vector2 fromCenterDir = rigidbody.velocity.normalized;
		float angle = Vector2.Angle(Vector2.right, -fromCenterDir)*Mathf.Deg2Rad*Mathf.Sign(-fromCenterDir.y) + Mathf.PI/2;
		transform.rotation = new Quaternion(0, 0, 1 * Mathf.Sin (angle/2), Mathf.Cos (angle/2));
	}

	public void UpdateGoals()
	{
		foreach (LevelGoal lg in shipGoals)
		{
			if(!_completed)
			{
				_completed = true;
				_completed &= lg.completed;
			}
			if(_completed)
			{
				ShipComplete();
			}
		}
	}

	void ShipComplete()
	{
		Debug.Log("Ship " + GetInstanceID() + " completed!");
		GameController.control.level.UpdateLevel(); //TODO Get level
	}
}
