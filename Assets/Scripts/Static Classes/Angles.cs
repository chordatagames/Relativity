using UnityEngine;
using System.Collections;

public static class Angles
{
	public static void RotateTransform(Transform t, float deltaAngle)
	{
		float angle = (t.eulerAngles.z + deltaAngle) * Mathf.Deg2Rad;
		t.rotation = new Quaternion(0, 0, Mathf.Sin (angle/2), Mathf.Cos (angle/2));
	}

	public static Vector2 RotateVector (Vector2 vec, float deltaAngle, bool usingRadians = false)
	{
		float radDegConverter = 1;
		if (!usingRadians)
		{
			radDegConverter = Mathf.Deg2Rad;
		}
		vec = CartesianToPolar(vec);
		vec = PolarToCartesian(new Vector2 (vec.x + deltaAngle * radDegConverter, vec.y));
		return vec;
	}

	public static Vector2 CartesianToPolar (Vector2 vec)
	{
		return new Vector2 (Vector2.Angle(Vector2.right, vec), vec.magnitude);
	}

	public static Vector2 PolarToCartesian (Vector2 vec)
	{
		float x = Mathf.Cos(vec.x) * vec.y;
		float y = Mathf.Sin(vec.x) * vec.y;
		return new Vector2 (x, y);
	}
}

