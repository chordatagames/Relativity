using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ConditionType))]
[CanEditMultipleObjects]
public class ConditionTypeEditor : Editor
{
	int i;
	string desc{ get { return (i==0) ? ("under") : ((i==1) ? "between" : "over"); } }
	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		serializedObject.Update();
		SerializedProperty iterator = serializedObject.GetIterator();
		bool enterChildren = true;
		bool inArray = false;
		i = 0;
		while (iterator.NextVisible(enterChildren))
		{

			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;
			if(iterator.propertyType == SerializedPropertyType.ObjectReference)
			{
				EditorGUILayout.SelectableLabel( target.GetType().ToString()+": "+target.name );
			}
			else if(iterator.propertyType == SerializedPropertyType.String && !inArray)
			{
				iterator.stringValue = EditorGUILayout.TextField(iterator.name,iterator.stringValue);
			}
			else if(inArray)
			{
				iterator.stringValue = EditorGUILayout.TextField(desc, iterator.stringValue);
				i++;
				inArray=(i<3);
			}
			else if (iterator.isArray)
			{
				inArray=true;
				iterator.NextVisible(true);
			}
			else
			{
				EditorGUILayout.PropertyField(iterator, true, new GUILayoutOption[0]);
			}
			enterChildren = false;
			EditorGUI.indentLevel = indent;
		}
		serializedObject.ApplyModifiedProperties();
		EditorGUI.EndChangeCheck();
	}
}
