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

	delegate void GoalCheck( GameObject testing );
	GoalCheck gc;

	void Start()
	{
		//Setup what to test
		for(int i=1; i < Mathf.Pow(2, System.Enum.GetNames( typeof(LevelGoal.LevelGoalCondition) ).Length); i+=i )
		{
			switch ((int)goalCondition & i) 
			{
			case (int)LevelGoal.LevelGoalCondition.AREA:
				gc += TestArea;
				break;
			case (int)LevelGoal.LevelGoalCondition.AGE:
				gc += TestAge;
				break;
			case (int)LevelGoal.LevelGoalCondition.SPEED:
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
			//_completed may change goaltesting
			gc(tracking); //itterate through objects?
		}
		if(_completed)
		{
			gc = null;
		}
	}

	//within min and max range
	public void TestArea(GameObject go)
	{
		Debug.Log("Area true!");
		Debug.Log((go.transform.position-transform.position).magnitude);
		_completed &= (go.transform.position-transform.position).magnitude > requiredRadius.x && 
			(go.transform.position-transform.position).magnitude < requiredRadius.y;
	}

	public void TestAge(GameObject go)
	{
		Debug.Log("Age true!");
		float age = go.GetComponent<PlayerBehaviour>().time;
		_completed &= age > requiredAge.x && age < requiredAge.y;
	}
	public void TestSpeed(GameObject go)
	{
		Debug.Log("Speed true!");
		float speed = go.GetComponent<Rigidbody2D>().velocity.magnitude;
		_completed &= speed > requiredSpeed.x && speed < requiredSpeed.y;
	}

	[System.Flags]
	public enum LevelGoalCondition
	{
		AGE 	= 1,	//0x00000001
		AREA	= 2,	//0x00000010
		SPEED	= 4,	//0x00000100
	}
}