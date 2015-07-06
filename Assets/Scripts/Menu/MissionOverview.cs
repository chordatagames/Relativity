using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public sealed class MissionOverview : MonoBehaviour {
	
	public GameObject goalViewPrefab, conditionViewPrefab; //The standard UI-elements for goals and conditions

	public Transform goalsPanel;
	Dictionary<LevelGoal,GameObject> GoalUIConnection = new Dictionary<LevelGoal, GameObject>();
	Dictionary<LevelGoalCondition,GameObject> ConditionUIConnection = new Dictionary<LevelGoalCondition, GameObject>();

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
				GoalUIConnection.Add(goal,go);
				go.transform.SetParent(goalsPanel);
				//TODO GENERATE NAMES BASED ON TYPE AND SHIP go.name =
				foreach (LevelGoalCondition condition in goal.conditions)
				{
					GameObject c = Instantiate<GameObject>(conditionViewPrefab);
					ConditionUIConnection.Add(condition,c);
					c.transform.SetParent(go.transform.FindChild("Conditions").transform);

					c.transform.FindChild("Label").GetComponent<Text>().text = condition.ToString();
				}
			}
		}
	}
	GameObject panel;
	// Update is called once per frame
	void Update () 
	{
		foreach(LevelGoal lg in GoalUIConnection.Keys)
		{
			if(GoalUIConnection.TryGetValue(lg, out panel))
			{
				panel.transform.GetChild(0).GetChild(0).GetComponent<Toggle>().isOn = lg.completed;

				Image img = panel.GetComponent<Image>();
				if (lg.completed)
				{
					img.color = Color.green; new Color(0.8f,0.5f,0.5f);
					foreach (LevelGoalCondition goalCondition in lg.conditions)
					{
						if(ConditionUIConnection.TryGetValue(goalCondition, out panel))
						{
							img = panel.GetComponent<Image>();
							img.color = Color.green;
						}
					}
				}
				else
				{
					img.color = new Color(0.8f,0.5f,0.5f);
				}
			}
		}
		foreach(LevelGoalCondition lgc in ConditionUIConnection.Keys)
		{
			if(ConditionUIConnection.TryGetValue(lgc, out panel))
			{
				panel.transform.GetChild(0).GetComponent<Toggle>().isOn = lgc.completed;
			}
		}
	}
}
