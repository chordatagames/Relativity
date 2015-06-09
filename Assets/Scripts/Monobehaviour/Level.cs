using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
	public Ship[] ships;
	public int blackholeLimit;  
	public Rect levelBounds;

	bool _completed;
	public bool completed { get { return _completed; } }

	public void UpdateLevel()
	{
		if (!_completed)
		{
			_completed = true;
			foreach (Ship ship in ships)
			{
				_completed &= ship.completed;
			}
		}

		if(_completed)
		{
			LevelComplete();
		}
	}

	void LevelComplete()
	{

	}
}