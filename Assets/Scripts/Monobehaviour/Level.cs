using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
	public Ship[] ships;
	public int blackholeLimit;  
	public Rect levelBounds;
	public Rect levelArea{get {return new Rect((Vector2)transform.position+levelBounds.position, levelBounds.size);}}

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

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(0.4f,1.0f,0.4f,0.2f);
		DrawBounds();
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		DrawBounds();
	}
	private void DrawBounds()
	{
		Gizmos.DrawLine(
			transform.position + new Vector3(levelBounds.xMax, levelBounds.y), 
			transform.position + new Vector3(levelBounds.x, levelBounds.y));
		Gizmos.DrawLine(
			transform.position + new Vector3(levelBounds.x, levelBounds.yMax), 
			transform.position + new Vector3(levelBounds.x, levelBounds.y));
		Gizmos.DrawLine(
			transform.position + new Vector3(levelBounds.xMax, levelBounds.yMax), 
			transform.position + new Vector3(levelBounds.x, levelBounds.yMax));
		Gizmos.DrawLine(
			transform.position + new Vector3(levelBounds.xMax, levelBounds.yMax), 
			transform.position + new Vector3(levelBounds.xMax, levelBounds.y));
	}
}