
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
                bool isNull = data.Field.GetValue(data.Target).IsUnityNull();

                GUIContent label = new GUIContent(fieldName, data.Property.tooltip);
                GUIContent toggle = new GUIContent(DrawerUtilityStyle.GetInstance().Null, fieldName + (isNull ? " is null." : " is not null."));

                EditorGUILayout.PrefixLabel(label);
                GUILayout.Toggle(isNull, toggle);
                GUI.enabled = enabled;
            }
        };
    }
}