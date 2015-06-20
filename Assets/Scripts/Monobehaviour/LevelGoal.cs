using UnityEngine;
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

	Transform AreaUI;

	public Vector2 requiredRadius;
	public Vector2 requiredAge;
	public Vector2 requiredSpeed;
	
	public GameObject tracking;
	public LevelGoalCondition goalCondition;

	public GoalState goalState;

	bool _completed = true;
	public bool completed { get { return _completed; } }

	delegate void GoalCheck();
	GoalCheck gc;

	void Start()
	{
		SetupAreaUI();
		goalState = GoalState.INCOMPLETE;

		//Setup what to test
		gc += InitTest;
		for(int i=1; i < Mathf.Pow(2, System.Enum.GetNames( typeof(LevelGoalCondition) ).Length); i+=i )
		{
			switch ((int)goalCondition & i) 
			{
			case (int)LevelGoalCondition.AREA:
				gc += TestArea;
				break;
			case (int)LevelGoalCondition.AGE:
				gc += TestAge;
				break;
			case (int)LevelGoalCondition.SPEED:
				gc += TestSpeed;
				break;
			default:
				break;
			}
		}
	}

	public LevelGoalCondition[] GetSeparateConditions()
	{
		List<LevelGoalCondition> conditions = new List<LevelGoalCondition>();
		for(int i=1; i < Mathf.Pow(2, System.Enum.GetNames( typeof(LevelGoalCondition) ).Length); i<<=1 )
		{
			if( ((int)goalCondition & i) == i) 
			{
				conditions.Add((LevelGoalCondition)i);
			}
		}
		return conditions.ToArray();
	}

	void Update()
	{
		//Perform tests
		if(gc != null)
		{
			//_completed may change in goaltesting
			gc(); //iterate through objects?
		}
		if(_completed)
		{
			gc = null;
			tracking.GetComponent<Ship>().UpdateGoals();
		}
		
		
	}

	void InitTest()
	{
		_completed = true;
	}
	//within min and max range
	void TestArea()
	{
		_completed &= (tracking.transform.position-transform.position).magnitude > requiredRadius.x && 
			(tracking.transform.position-transform.position).magnitude < requiredRadius.y;
		Debug.Log("area: " + _completed);
	}

	void TestAge()
	{
		float age = tracking.GetComponent<PlayerBehaviour>().time;
		_completed &= age > requiredAge.x && age < requiredAge.y;
		Debug.Log("age: " + _completed);
	}
	void TestSpeed()
	{
		float speed = tracking.GetComponent<Rigidbody2D>().velocity.magnitude;
		_completed &= speed > requiredSpeed.x && speed < requiredSpeed.y;
		Debug.Log("speed: " + _completed);
	}
	
	void SetupAreaUI ()
	{
		FindAreaUI();
		SetAreaUI();
	}
	
	void FindAreaUI ()
	{
		AreaUI = transform.FindChild("Canvas").FindChild("Goal Area"); //FIXME Use a public field instead
	}
	
	void SetAreaUI ()
	{
		AreaUI.localScale = 2.0f * new Vector3 (requiredRadius.y, requiredRadius.y, 0f);
	}

	[System.Flags]
	public enum LevelGoalCondition
	{
		AGE 	= 1,	//0x00000001
		AREA	= 2,	//0x00000010
		SPEED	= 4,	//0x00000100
	}
}