using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
	public static float gridSize = 0.5f;

	public static Vector2 Gridify( Vector2 point )
	{
		float xStep = point.x + gridSize/2;
		float yStep = point.y + gridSize/2;
		return new Vector2 ( xStep - (xStep%gridSize),yStep - (yStep%gridSize));
	}


}