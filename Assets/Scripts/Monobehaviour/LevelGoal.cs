using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class LevelGoal : MonoBehaviour
{

	public enum GoalState
	{
		FAILED,
		INCOMPLETE,
		COMPLETED
	}
	
	public GameObject tracking;

	public GoalState goalState;

	private List<LevelGoalCondition> 	_conditions = new List<LevelGoalCondition>();
	public LevelGoalCondition[] 		conditions { get { return _conditions.ToArray(); } }

	private bool _completed;
	public bool completed { get { return _completed; } }

	void Start()
	{
		foreach(LevelGoalCondition lgc in GetComponents<LevelGoalCondition>())
		{
			_conditions.Add(lgc);
		}
		goalState = GoalState.INCOMPLETE;
	
	}

	void CompletedTest()
	{
		_completed = true;
		foreach(LevelGoalCondition c in conditions)
		{
			c.conditionTester.Invoke(this);
			_completed &= c.completed;
		}
	}

	void Update()
	{
		if(_completed)
		{
			goalState=GoalState.COMPLETED;
			tracking.GetComponent<Ship>().UpdateGoals();
		}
		else
		{
			CompletedTest();
		}
	}

//	void SetupAreaUI ()
//	{
//		FindAreaUI();
//		SetAreaUI();
//	}
//	
//	void FindAreaUI ()
//	{
//		AreaUI = transform.FindChild("Canvas").FindChild("Goal Area"); //FIXME Use a public field instead
//	}
//	
//	void SetAreaUI ()
//	{
//		AreaUI.localScale = 2.0f * new Vector3 (requiredRadius.y, requiredRadius.y, 0f);
//	}
}
