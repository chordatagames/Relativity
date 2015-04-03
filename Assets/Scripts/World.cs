using UnityEngine;
using System.Collections;

public static class World
{
	static float _time = 0.0f;
	public static float time {get{return _time;}}

	public static void AddTime (float t)
	{
		_time += t;
	}
}
