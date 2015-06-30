using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class LevelGoalCondition : MonoBehaviour
{
	[System.Serializable]
	public class LevelGoalConditionTest : UnityEvent<LevelGoal>
	{}

	public ConditionType desc;//ConditionType description
	public float min, max;
	private bool _completed;
	public bool completed { get { return _completed; } }

	public LevelGoalConditionTest conditionTester;

	void AssignCompleted(float value)
	{
		_completed = (value>min && value<max) || (value>min && max<0); //between limits or over minlimit if max is "infinity".
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
