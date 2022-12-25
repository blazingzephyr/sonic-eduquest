
using System;
using UnityEditor;
using UnityEngine;

namespace SonicEduquest
{
    public static class PropertyDrawerUtility
    {
        public static Action<PropertyDrawerData>[] CustomDrawers => new Action<PropertyDrawerData>[]
        {
            data =>
            {
                bool enabled = GUI.enabled;
                GUI.enabled = false;

                string fieldName = data.Field.Name;
                string not = data.Field.GetValue(data.Target).IsUnityNull() ? String.Empty : "not";

                GUIContent label = new GUIContent($"{fieldName} is {not} null.", data.Property.tooltip);
                EditorGUILayout.LabelField(label);
                GUI.enabled = enabled;
            }
        };
    }
}