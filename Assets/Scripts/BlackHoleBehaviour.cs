using UnityEngine;
using System.Collections;

public class BlackHoleBehaviour : MonoBehaviour, IGameEditable
{
	public GameEditableValues Values { get; set; }

	public float mass;
	public float rotationSpeed;

	void Start()
	{
		WorldScene.Instance.dataEntries.Add(this);
	}

	void Update ()
	{
		Angles.RotateTransform(transform, Time.deltaTime * World.timeDilationFactor * rotationSpeed);
	}

	
	public void SetValues()
	{
		Values = new GameEditableValues(gameObject);
	}
	
	public void ResetValues()
	{
		transform.position 		= Values.position;
		transform.rotation		= Quaternion.Euler(Values.rotation);
		transform.localScale 	= Values.scale;
	}

}