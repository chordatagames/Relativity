using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public sealed class MissionOverview : MonoBehaviour {
	
	public GameObject goalViewPrefab, conditionViewPrefab; //The standard UI-elements for goals and conditions

	public Transform goalsPanel;

	Level level;

	// Use this for initialization
	void Start () 
	{
		level = GameController.control.level;
		foreach (Ship ship in level.ships)
		{
			foreach (LevelGoal goal in ship.shipGoals)
			{
				GameObject go = Instantiate<GameObject>(goalViewPrefab);
				go.transform.SetParent(goalsPanel);
				//TODO GENERATE NAMES BASED ON TYPE AND SHIP go.name =
				foreach (LevelGoalCondition c in goal.conditions)
				{
					GameObject cView = Instantiate<GameObject>(conditionViewPrefab);
					cView.transform.SetParent(go.transform.FindChild("Conditions").transform);
					Debug.Log(MissionBriefGenerator.ConditionBrief(c.desc, c.min, c.max));
					cView.transform.FindChild("Label").GetComponent<Text>().text = MissionBriefGenerator.ConditionBrief(c.desc, c.min, c.max);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}