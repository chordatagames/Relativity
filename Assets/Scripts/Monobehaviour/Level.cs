using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
	public Ship[] ships;
	public int blackholeLimit;

	bool _completed;
	public bool completed { get { return _completed; } }

	public void UpdateLevel()
	{
		foreach (Ship ship in ships)
		{
			if(!_completed)
			{
				_completed = true;
				_completed &= ship.completed;
			}
			if(_completed)
			{
				LevelComplete();
			}
		}
	}

	void LevelComplete()
	{
		//Menu-popup
	}
}