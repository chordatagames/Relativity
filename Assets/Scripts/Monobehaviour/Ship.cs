using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	public LevelGoal[] shipGoals;

	bool _completed;
	public bool completed { get { return _completed; } }

	void Start()
	{
		foreach (LevelGoal lg in shipGoals)
		{
			lg.tracking = gameObject;
		}
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
		transform.parent.GetComponent<Level>().UpdateLevel(); //TODO Get level
	}
}
