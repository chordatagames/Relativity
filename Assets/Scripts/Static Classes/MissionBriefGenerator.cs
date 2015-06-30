using UnityEngine;
using System.Collections;

public static class MissionBriefGenerator
{
	public static string ConditionBrief(ConditionType type, float min, float max)
	{
		return type.subject + " is " + type.prepositions[(min > 0) ? ((max > 0) ? 1 : 2) : 0] +" "+ ((min > 0) ? min:max) + type.unit + ((min > 0 && max > 0) ? " and " + max + type.unit:"");
	}
}