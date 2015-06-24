using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class World
{
	static float _time = 0.0f;
	public static float time {get{return _time;} set{_time = value;}}
	
	public static float gravityConstant = 10.0f;
	public static float timeDistortionConstant = 4.0f;
	public static float relativeTimeDilationFactor = 1.0f;

	public static void AddTime (float t)
	{
		_time += t;
	}

	public static void PassTime ()
	{
		AddTime(Time.deltaTime * relativeTimeDilationFactor);
	}

	public static void SetAllRelativeTimeDilationFactors()
	{
		GameController.control.currentShip.UpdateWorldRelativeTimeDilationFactor();
	}
}
