using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class LevelGoalCondition : MonoBehaviour
{
	[System.Serializable]
	public class LevelGoalConditionTest : UnityEvent<LevelGoal>
	{}

	public ConditionType typeDesc;//ConditionType description
	public float min, max;
	private bool _completed;
	public bool completed { get { return _completed; } }

	public LevelGoalConditionTest conditionTester;

	void FixedUpdate()
	{
		Debug.Log(this + " is " + (completed ? "" : "not ") + "completed");
	}

	public override string ToString ()
	{
		return MissionBriefGenerator.ConditionBrief(typeDesc,min,max);
	}

	void AssignCompleted(float value)
	{
		_completed = (value>this.min && value<this.max) || (value>this.min && this.max<=0); //between limits or over minlimit if max is "infinity".
	}

	public void AreaTest(LevelGoal lg)
	{
		float dist = (lg.tracking.transform.position-lg.transform.position).magnitude;
		AssignCompleted(dist);
	}

	public void AgeTest(LevelGoal lg)
	{
		float age = lg.tracking.GetComponent<PlayerBehaviour>().time;
		AssignCompleted(age);
	}

}
