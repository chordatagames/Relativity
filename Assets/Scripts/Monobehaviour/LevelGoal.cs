using UnityEngine;
using System.Collections;

public class LevelGoal : MonoBehaviour
{
	public Vector2 requiredRadius;
	public Vector2 requiredAge;
	public Vector2 requiredSpeed;
	
	public GameObject tracking;
	public LevelGoalCondition goalCondition;

	bool _completed = true;
	public bool completed { get { return _completed; } }

	delegate void GoalCheck();
	GoalCheck gc;

	void Start()
	{
		//Setup what to test
		gc += InitTest;
		for(int i=1; i < Mathf.Pow(2, System.Enum.GetNames( typeof(LevelGoalCondition) ).Length); i+=i )
		{
			switch ((int)goalCondition & i) 
			{
			case (int)LevelGoalCondition.AREA:
				Debug.Log("testing area");
				gc += TestArea;
				break;
			case (int)LevelGoalCondition.AGE:
				Debug.Log("testing age");
				gc += TestAge;
				break;
			case (int)LevelGoalCondition.SPEED:
				Debug.Log("testing speed");
				gc += TestSpeed;
				break;
			default:
				break;
			}
		}
	}

	void Update()
	{
		//Perform tests
		if(gc != null)
		{
			//_completed may change in goaltesting
			gc(); //itterate through objects?
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

	[System.Flags]
	public enum LevelGoalCondition
	{
		AGE 	= 1,	//0x00000001
		AREA	= 2,	//0x00000010
		SPEED	= 4,	//0x00000100
	}
}