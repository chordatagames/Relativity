using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
	public static float gridSize = 1.0f;

	public static Vector2 Gridify( Vector2 point )
	{
		float xStep = Mathf.Floor((point.x / gridSize) + 0.5f);
		float yStep = Mathf.Floor((point.y / gridSize) + 0.5f);
		return new Vector2 (xStep * gridSize, yStep * gridSize);
	}
	
	public static Vector3 Gridify3( Vector2 point )
	{
		float xStep = Mathf.Floor((point.x / gridSize) + 0.5f);
		float yStep = Mathf.Floor((point.y / gridSize) + 0.5f);
		return new Vector3 (xStep * gridSize, yStep * gridSize, 0);
	}
}