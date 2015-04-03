using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class World
{
	static float _time = 0.0f;
	public static float time {get{return _time;}}

	public static float timeDilationFactor = 1.0f;

	public static void AddTime (float t)
	{
		_time += t;
	}

	public static void PassTime ()
	{
		AddTime(Time.deltaTime * timeDilationFactor);
	}

}
