using System;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
using UnityEditor;
using UnityEditorInternal;

[CustomPropertyDrawer (typeof (BehaviourStateCondition))]
public class BehaviourStateConditionDrawer : PropertyDrawer {

    ReorderableList list;
    GUIContent buttonContent;

    const int spacing = 5;
    const int extraSpacing = 9;
    const string noFunctionString = "No Function";

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {

        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty (position, label, property);
        
        Rect conditionStateRect = new Rect (position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        if (list == null) {
            list = BuildConditionStateReorderableList (property.FindPropertyRelative ("conditionStatePairs"), property.displayName);
        }
        list.DoList (conditionStateRect);

        EditorGUI.EndProperty ();
    }

    ReorderableList BuildConditionStateReorderableList (SerializedProperty property, string displayName) {
        ReorderableList list = new ReorderableList (property.serializedObject, property, true, true, true, true);
        
        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.indentLevel = 0;
            EditorGUI.LabelField (rect, displayName);
        };

		list.elementHeight = EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing + extraSpacing;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
			EditorGUI.PropertyField (rect, property.GetArrayElementAtIndex (index), GUIContent.none, true);
		};

        return list;
    }

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
        float height = 0f;
        if (list != null) {
            height = list.GetHeight ();
        }

        return height;
    }
	
}
