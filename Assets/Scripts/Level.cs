using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level: MonoBehaviour
{
	public LevelGoal[]	levelGoals;

	public void setLevelGoals(params LevelGoal[] goals)
	{
		List<LevelGoal> lgList = new List<LevelGoal>();
		foreach (LevelGoal lg in goals)
		{
			lgList.Add(lg);
		}
		levelGoals = lgList.ToArray();
	}
}