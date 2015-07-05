using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu]
public class LevelContainer : ScriptableObject 
{
	public LevelObject[] levelObjects;
}

public class LevelObject
{
	public Dictionary<string,object> objectValues;
}