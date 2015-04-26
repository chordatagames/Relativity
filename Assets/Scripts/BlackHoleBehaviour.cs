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

	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Ship")
		{
			Debug.Log("Pulling ship");
			Rigidbody2D colrig = col.transform.GetComponent<Rigidbody2D> ();
			Vector2 force = (Vector2)(transform.position - col.transform.position).normalized
				* col.transform.GetComponent<PlayerBehaviour> ().gravityConstant
				* mass
				* colrig.mass
				* Mathf.Pow ((transform.position - col.transform.position).magnitude, -2);
			colrig.AddForce(force);
			col.transform.GetComponent<PlayerBehaviour>().addTimeDilationForce(force);
		}
	}
}