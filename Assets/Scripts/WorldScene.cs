using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class WorldScene
{
	public static 	Dictionary<Vector3, GameObject> 	attractors = Dictionary<Vector3, GameObject>();

	public static 	Dictionary<Vector3, GameObject> 	ships = Dictionary<Vector3, GameObject>();

	public static void AddAttractor(GameObject attractor)
	{
		attractors.Add(attractor.transform.position, attractor);
	}
	public static void RemoveAttractor(GameObject attractor)
	{
		attractors.Remove(attractor.transform.position);
	}
	public static void AddShip(GameObject ship)
	{
		ships.Add(ship.transform.position,ship);
	}
	public static void RemoveShip(GameObject ship)
	{
		ships.Remove(ship.transform.position);
	}
}
