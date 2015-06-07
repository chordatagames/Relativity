using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldScene
{
	// Singleton pattern assignment.
	private static WorldScene instance;
	public static WorldScene Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new WorldScene();
			}
			return instance;
		}
	}

	public List<IGameEditable> dataEntries = new List<IGameEditable>();

	public void ClearDataEntries ()
	{
		dataEntries = new List<IGameEditable>();
	}

	public void SetValues()
	{
		foreach(IGameEditable editable in dataEntries.ToArray())
		{
			editable.SetValues();
		}
	}

	public void ResetValues()
	{
		foreach(IGameEditable editable in dataEntries.ToArray())
		{
			editable.ResetValues();
		}
	}
}

public interface IGameEditable
{
	GameEditableValues Values {get; set;}

	void SetValues();
	void ResetValues();
}

public struct GameEditableValues
{
	public Vector3 position, rotation, scale;
	public Vector2 velocity;

	public GameEditableValues(GameObject gameObject)
	{
		position 	= gameObject.transform.position;
		rotation 	= gameObject.transform.rotation.eulerAngles;
		scale 		= gameObject.transform.localScale;
		if ( gameObject.GetComponent<Rigidbody2D>() )
		{
			velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
		}
		else
		{
			velocity = Vector2.zero;
		}
	}
}