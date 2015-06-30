using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[CreateAssetMenu]
[System.Serializable]
public class ConditionType : ScriptableObject
{
	public string subject;
	public string[] prepositions = new string[3]; 
	public string unit;
}