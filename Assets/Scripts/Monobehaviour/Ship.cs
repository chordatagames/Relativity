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
		// rotate sprite according to direction of velocity
		Vector2 fromCenterDir = rigidbody.velocity.normalized;
		float angle = Vector2.Angle(Vector2.right, -fromCenterDir)*Mathf.Deg2Rad*Mathf.Sign(-fromCenterDir.y) + Mathf.PI/2;
		sprite.transform.rotation = new Quaternion(0, 0, 1 * Mathf.Sin (angle/2), Mathf.Cos (angle/2));

		// check if ship is outside level bounds
		if (!GameController.control.level.levelBounds.Contains(transform.position))
		{
			print("Ship " + GetInstanceID() + " out of bounds.");
			foreach (LevelGoal goal in shipGoals)
			{
				if (goal.goalState == LevelGoal.GoalState.INCOMPLETE)
					goal.goalState = LevelGoal.GoalState.FAILED;
			}
		}
	}

	public void UpdateGoals()
	{
		foreach (LevelGoal lg in shipGoals)
		{
			if(!_completed)
			{
				_completed = true;
				_completed &= (lg.completed && lg.goalState != LevelGoal.GoalState.FAILED);
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
