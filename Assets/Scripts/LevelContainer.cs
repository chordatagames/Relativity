using UnityEngine;
using System;
using System.Collections;

[Serializable]
[CreateAssetMenu]
public class LevelContainer : ScriptableObject 
{
	public LevelObject[] levelObjects;
}

[Serializable]
public class ValueType 
{
	public string key;
	public String[] value;
}

[Serializable]
public class LevelObject
{
	public ValueType[] levelObjectValues;
}