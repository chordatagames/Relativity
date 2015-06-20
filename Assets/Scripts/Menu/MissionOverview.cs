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
				foreach (LevelGoal.LevelGoalCondition condition in goal.GetSeparateConditions())
				{
					GameObject c = Instantiate<GameObject>(conditionViewPrefab);
					c.transform.SetParent(go.transform.FindChild("Conditions").transform);

					c.transform.FindChild("Label").GetComponent<Text>().text = condition.ToString();
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
