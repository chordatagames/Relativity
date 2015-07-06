using UnityEngine;
using System.Collections;

public class BlackHoleBehaviour : MonoBehaviour, IGameEditable
{
	public GameEditableValues Values { get; set; }

	public float mass;
	public float rotationSpeed;
	public float areaOfEffectRadius;

	CircleCollider2D areaOfEffect;
	Transform areaOfEffectUI;

	void Start()
	{
		areaOfEffect = GetComponent<CircleCollider2D>();
		areaOfEffectUI = transform.FindChild("Canvas").FindChild("Area of Effect");
		UpdateAreaOfEffectSize();
		WorldScene.Instance.dataEntries.Add(this);
	}

	void Update ()
	{
		Angles.RotateTransform(transform, Time.deltaTime * World.relativeTimeDilationFactor * rotationSpeed);
		UpdateAreaOfEffectSize ();
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
			Vector2 force = GetGravityForce(col.transform);
			col.transform.GetComponent<PlayerBehaviour>().AddForce(force);
			col.transform.GetComponent<PlayerBehaviour>().AddTimeDilationForce(force);
		}
	}

	Vector2 GetGravityForce(Transform attracted)
	{
		Vector2 relation = (transform.position - attracted.position);
		return relation.normalized
				* World.gravityConstant 
				* mass
				* attracted.GetComponent<Rigidbody2D>().mass
				* Mathf.Pow(relation.magnitude, -2);
	}

	public static Vector2 GetPotentialGravityForce(GameObject attracted, Vector3 potentialPos)
	{
		Collider2D obj = Physics2D.OverlapCircle(potentialPos, 0.01f);
		BlackHoleBehaviour bh = (obj != null) ? obj.GetComponent<BlackHoleBehaviour>() : null;
		if (bh != null)
		{
			Vector2 relation = (bh.transform.position - attracted.transform.position);
			return relation.normalized
				* World.gravityConstant 
				* bh.mass
				* attracted.GetComponent<Rigidbody2D>().mass
				* Mathf.Pow(relation.magnitude, -2);
		}
		return Vector2.zero;
	}

	void UpdateAreaOfEffectSize()
	{
		areaOfEffect.radius = areaOfEffectRadius;
		areaOfEffectUI.localScale = new Vector3 (areaOfEffectRadius * 2, areaOfEffectRadius * 2, 0);
	}
}