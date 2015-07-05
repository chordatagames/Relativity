using UnityEngine;
using System.Collections;

public abstract class GameObstacle : MonoBehaviour 
{
	public bool editable = false;
	// returns obstacle's values in a friendly format
	public abstract ValueType[] GetValues();
	// sets obstacle's values from asset file
	public abstract void SetValues(ValueType[] vals);
	public abstract void ResetValues();
}