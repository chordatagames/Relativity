using UnityEngine;
using System.Collections;

public class LevelGoal : MonoBehaviour
{
	public Vector2 requiredRadius;
	public Vector2 requiredAge;
	public Vector2 requiredSpeed;
	public LevelGoalCondition goalCondition;
	public GameObject tracking;


	bool completed = false;
	delegate bool GoalCheck( GameObject testing );
	GoalCheck gc;
	void Start()
	{
		//Setup what to test
		for(int i=1; i < Mathf.Pow(2, System.Enum.GetNames( typeof(LevelGoal.LevelGoalCondition) ).Length); i+=i )
		{
			switch (goalConditionProp.intValue & i) 
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
			gc(tracking); //itterate through objects?
		}
	}

	//within min and max range
	public bool TestArea(GameObject go)
	{
		return (go.transform.position-this.transform.position).magnitude >= this.requiredRadius.x && 
			(go.transform.position-this.transform.position).magnitude <= this.requiredRadius.y;
	}

	public bool TestAge(GameObject go)
	{
		return (go.transform.position-this.transform.position).magnitude >= this.requiredRadius.x && 
			(go.transform.position-this.transform.position).magnitude <= this.requiredRadius.y;
	}
	public bool TestSpeed(GameObject go)
	{
		return (go.transform.position-this.transform.position).magnitude >= this.requiredRadius.x && 
			(go.transform.position-this.transform.position).magnitude <= this.requiredRadius.y;
	}

	[System.Flags]
	public enum LevelGoalCondition
	{
		AGE 	= 1,	//0x00000001
		AREA	= 2,	//0x00000010
		SPEED	= 4		//0x00000100
	}
}
//AGING CLASS?