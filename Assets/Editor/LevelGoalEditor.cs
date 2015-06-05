using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LevelGoal))]
public class LevelGoalEditor : Editor
{
	SerializedProperty radiusProp;
	SerializedProperty ageProp;
	SerializedProperty speedProp;
	SerializedProperty goalConditionProp;
	
	void OnEnable ()
	{
		// Setup the SerializedProperties
		radiusProp = 		serializedObject.FindProperty ("requiredRadius");
		ageProp = 			serializedObject.FindProperty ("requiredAge");
		speedProp = 		serializedObject.FindProperty ("requiredSpeed");

		goalConditionProp = serializedObject.FindProperty ("goalCondition");
	}

	public override void OnInspectorGUI() 
	{
		base.DrawDefaultInspector();
		// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		serializedObject.Update ();

		for(int i=1; i < Mathf.Pow(2, System.Enum.GetNames( typeof(LevelGoal.LevelGoalCondition) ).Length); i+=i )
		{
			switch (goalConditionProp.intValue & i) 
			{
			case (int)LevelGoal.LevelGoalCondition.AREA:
				radiusProp.vector2Value = EditorGUILayout.Vector2Field("Radius Required", radiusProp.vector2Value); 
				break;
			case (int)LevelGoal.LevelGoalCondition.AGE:
				ageProp.vector2Value = EditorGUILayout.Vector2Field ("Age Required", ageProp.vector2Value); 
				break;
			case (int)LevelGoal.LevelGoalCondition.SPEED:
				speedProp.vector2Value = EditorGUILayout.Vector2Field ("Speed Required", speedProp.vector2Value); 
				break;
			default:
				break;
			}
		}

		goalConditionProp.intValue = (int)((LevelGoal.LevelGoalCondition)EditorGUILayout.EnumMaskField("Conditions", (LevelGoal.LevelGoalCondition)goalConditionProp.intValue));

		// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
		serializedObject.ApplyModifiedProperties ();
	}

}
