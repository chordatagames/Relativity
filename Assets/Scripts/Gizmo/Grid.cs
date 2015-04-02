using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
	public static float gridSize = 1f;

	public static Vector2 Gridify( Vector2 point )
	{
		return new Vector2 ( Mathf.Round(point.x/gridSize), Mathf.Round(point.y/gridSize) );
	}


}