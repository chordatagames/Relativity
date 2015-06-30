using UnityEngine;
using System.Collections;

public static class MissionBriefGenerator
{
	public static string ConditionBrief(ConditionType type, float min, float max)
	{
		//return type.subject + " is " + type.prepositions[(min > 0) ? ((max > 0) ? 1 : 2) : 0] +" "+ ((min > 0) ? min:max) + type.unit + ((min > 0 && max > 0) ? " and " + max + type.unit:"");
		string brief = type.subject + " is ";
		if(min > 0)
		{
			if(max > 0)
			{
				brief += type.prepositions[1]; //between
				brief += " " + min + type.unit + " and " + max + type.unit;
			}
			else
			{
				brief += type.prepositions[2]; //over
				brief += " " + min + type.unit;
			}

		}
		else
		{
			brief += type.prepositions[0]; //under 
			brief += " " + max + type.unit;
		}

		return brief;
	}
	public static string ConditionBrief(ConditionType type, float min, float max, GameObject target)
	{
		return type.subject + " is " + type.prepositions[(min > 0) ? ((max > 0) ? 1 : 2) : 0] +" "+ ((min > 0) ? min:max) + type.unit + ((min > 0 && max > 0) ? " and " + max + type.unit:"");
	}
}