using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer (typeof (ConditionStatePair))]
public class ConditionStatePairDrawer : SerializableCallbackDrawer {

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		
		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		position.y += EditorGUIUtility.standardVerticalSpacing;

		SerializedProperty condition = property.FindPropertyRelative ("ConditionPassed");

		// Using BeginProperty / EndProperty on the parent property means that
		// prefab override logic works on the entire property.
		EditorGUI.BeginProperty (position, label, property);
		// Draw label
		Rect pos = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), new GUIContent ("Source"));

		Rect targetRect = new Rect (pos.x, pos.y, pos.width, EditorGUIUtility.singleLineHeight);
		
		// Get target
		SerializedProperty targetProp = condition.FindPropertyRelative ("_target");
		object target = targetProp.objectReferenceValue;
		EditorGUI.PropertyField (targetRect, targetProp, GUIContent.none);

		Rect resultRect = new Rect (position.x, pos.y, position.width, EditorGUIUtility.singleLineHeight);
		bool resultEnabled = true;

		if (target != null) {
			// Get method name
			SerializedProperty methodProp = condition.FindPropertyRelative ("_methodName");
			string methodName = methodProp.stringValue;

			// Get args
			SerializedProperty argProps = condition.FindPropertyRelative ("_args");
			Type[] argTypes = GetArgTypes (argProps);

			// Get dynamic
			SerializedProperty dynamicProp = condition.FindPropertyRelative ("_dynamic");
			bool dynamic = dynamicProp.boolValue;

			// Get active method
			MethodInfo activeMethod = GetMethod (target, methodName, argTypes);

			GUIContent methodlabel = new GUIContent ("No Function");
			if (activeMethod != null) methodlabel = new GUIContent (PrettifyMethod (activeMethod));
			else if (!string.IsNullOrEmpty (methodName)) methodlabel = new GUIContent ("Missing (" + PrettifyMethod (methodName, argTypes) + ")");

			Rect methodRect = new Rect (position.x, targetRect.max.y + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);

			// Method select button
			pos = EditorGUI.PrefixLabel (methodRect, GUIUtility.GetControlID (FocusType.Passive), new GUIContent (dynamic ? condition.displayName + " (dynamic)" : condition.displayName));
			if (EditorGUI.DropdownButton (pos, methodlabel, FocusType.Keyboard)) {
				MethodSelector (condition);
			}

			if (activeMethod != null && !dynamic) {
				// Args
				ParameterInfo[] activeParameters = activeMethod.GetParameters ();
				Rect argRect = new Rect (position.x, methodRect.max.y + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
				string[] types = new string[argProps.arraySize];
				for (int i = 0; i < types.Length; i++) {
					SerializedProperty argProp = argProps.FindPropertyRelative ("Array.data[" + i + "]");
					GUIContent argLabel = new GUIContent (ObjectNames.NicifyVariableName (activeParameters[i].Name));

					EditorGUI.BeginChangeCheck ();
					switch ((Arg.ArgType)argProp.FindPropertyRelative ("argType").enumValueIndex) {
						case Arg.ArgType.Bool:
							EditorGUI.PropertyField (argRect, argProp.FindPropertyRelative ("boolValue"), argLabel);
							break;
						case Arg.ArgType.Int:
							EditorGUI.PropertyField (argRect, argProp.FindPropertyRelative ("intValue"), argLabel);
							break;
						case Arg.ArgType.Float:
							EditorGUI.PropertyField (argRect, argProp.FindPropertyRelative ("floatValue"), argLabel);
							break;
						case Arg.ArgType.String:
							EditorGUI.PropertyField (argRect, argProp.FindPropertyRelative ("stringValue"), argLabel);
							break;
						case Arg.ArgType.Object:
							EditorGUI.PropertyField (argRect, argProp.FindPropertyRelative ("objectValue"), argLabel);
							break;
					}
					if (EditorGUI.EndChangeCheck ()) {
						condition.FindPropertyRelative ("dirty").boolValue = true;
					}
					argRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
				}
			}
			resultRect.y = methodRect.max.y + EditorGUIUtility.standardVerticalSpacing;
		} else {
			Rect helpBoxRect = new Rect (position.x + 8, targetRect.max.y + EditorGUIUtility.standardVerticalSpacing, position.width - 16, EditorGUIUtility.singleLineHeight);
			string msg = " Source object required!";
			EditorGUI.LabelField (helpBoxRect, new GUIContent (msg, msg), "helpBox");
			resultRect.y = helpBoxRect.max.y + EditorGUIUtility.standardVerticalSpacing;
			resultEnabled = false;
		}

		bool wasEnabled = GUI.enabled;
		GUI.enabled = resultEnabled;
		EditorGUI.PropertyField (resultRect, property.FindPropertyRelative ("decisionState"), true);
		GUI.enabled = wasEnabled;
		
		// Set indent back to what it was
		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty ();
	}

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		float lineheight = EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;
		return lineheight + base.GetPropertyHeight (property.FindPropertyRelative ("ConditionPassed"), label);
	}
}
