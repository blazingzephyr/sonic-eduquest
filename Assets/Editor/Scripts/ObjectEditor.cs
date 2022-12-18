
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using HelpBoxList = System.Collections.Generic.List<(string Label, UnityEditor.MessageType MessageType, SonicEduquest.ObjectEditor.FieldColorData Coloring)>;

namespace SonicEduquest
{
    [CustomEditor(typeof(object), true)]
    public class ObjectEditor : Editor
    {
        private Type                    _inspectedType;
        private FieldInfo[]             _fields;
        private List<SerializedField>   _serializedFields;
        private Color                   _previousBackground;
        private Color                   _previousForeground;

        public record FieldColorData
        {
            public  Color   Background          { get; set; } = GUI.backgroundColor;
            public  Color   Foreground          { get; set; } = GUI.contentColor;
            public  Color   HelpBoxBackground   { get; set; } = GUI.backgroundColor;
            public  Color   HelpBoxForeground   { get; set; } = GUI.contentColor;
        }

        public class SerializedField
        {
            public  FieldInfo                   Field               { get; set; } = null;
            public  SerializedProperty          Property            { get; set; } = null;
            public  bool                        IsReadOnly          { get; set; } = false;
            public  bool                        IsRequired          { get; set; } = false;
            public  Action<PropertyDrawerData>  Action              { get; set; } = null;
            public  Type[]                      ConcreteTypes       { get; set; } = Array.Empty<Type>();
            public  string[]                    ConcreteTypenames   { get; set; } = Array.Empty<string>();
            public  string                      Header              { get; set; } = string.Empty;
            public  string                      HeaderTooltip       { get; set; } = string.Empty;
        }

        public override void OnInspectorGUI()
        {
            if (this._serializedFields == null)
            {
                LoadData();
            }

            this.serializedObject.Update();
            EditorStyle style = EditorStyle.GetInstance();

            HelpBoxList helpBoxes = new HelpBoxList();
            foreach (SerializedField field in this._serializedFields)
            {
                bool hasHeader = !String.IsNullOrEmpty(field.Header);
                if (hasHeader)
                {
                    EditorGUILayout.Space(style.PreHeaderSpace);
                    GUIContent header = new GUIContent(field.Header, field.HeaderTooltip);
                    GUILayout.Label(header, EditorStyles.boldLabel);
                    EditorGUILayout.Space(style.PostHeaderSpace);
                }

                bool enabled = GUI.enabled;
                GUI.enabled = !field.IsReadOnly && enabled;

                object value = field.Field.GetValue(target);
                bool isNull = value.IsUnityNull();

                string fieldName = field.Property.name;
                (FieldColorData coloring, Texture icon, string iconTooltip) = new { isNull, field.IsRequired, field.IsReadOnly } switch
                {
                    { IsReadOnly: true } => (new FieldColorData(), style.ReadOnly, fieldName + " is read-only."),

                    { isNull: true, IsRequired: true }
                        =>
                        (
                            new FieldColorData
                            {
                                Background = style.BackgroundRequiredNull,
                                Foreground = style.ForegroundRequiredNull,
                                HelpBoxBackground = style.HelpBoxBackgroundRequiredNull,
                                HelpBoxForeground = style.HelpBoxForegroundRequiredNull
                            },
                            style.RequiredNull,
                            fieldName + " must be assigned and cannot be null."
                        ),

                    { isNull: true }
                        =>
                        (
                            new FieldColorData
                            {
                                Background = style.BackgroundNull,
                                Foreground = style.ForegroundNull,
                                HelpBoxBackground = style.HelpBoxBackgroundNull,
                                HelpBoxForeground = style.HelpBoxForegroundNull
                            },
                            style.Null,
                            fieldName + " is null."
                        ),

                    { IsRequired: true }
                        =>
                        (
                            new FieldColorData
                            {
                                Background = style.BackgroundRequired,
                                Foreground = style.ForegroundRequired
                            },
                            style.Required,
                            fieldName + " must be assigned."
                        ),

                    { isNull: false, IsRequired: false } => (new FieldColorData(), style.Member, fieldName + ".")
                };

                StartColoring(coloring.Background, coloring.Foreground);
                GUIContent iconContent = new GUIContent(icon, iconTooltip);
                GUILayout.Label(iconContent);
                EditorGUILayout.Space(style.PostIconSpace);

                GUIContent labelContent = new GUIContent(fieldName, field.Property.tooltip);
                EditorGUILayout.PropertyField(field.Property, labelContent, true);
                EndColoring();
                EditorGUILayout.Space(style.PostPropertySpace);

                if (field.ConcreteTypes.Length > 0)
                {
                    EditorGUILayout.Space(style.PreCovariantFieldSpace);

                    int index = Array.IndexOf(field.ConcreteTypes, value.GetType());
                    int selectedIndex = GUILayout.SelectionGrid(index, field.ConcreteTypenames, 4);
                    EditorGUILayout.Space(style.PostCovariantFieldSpace);

                    if (index != selectedIndex)
                    {
                        field.Property.managedReferenceValue = Activator.CreateInstance(field.ConcreteTypes[selectedIndex]);
                    }
                }

                GUI.enabled = enabled;
                if (isNull)
                {
                    helpBoxes.Add(
                        field.IsRequired ?
                            (fieldName + " cannot be null.",   MessageType.Error,      coloring) :
                            (fieldName + " is null.",         MessageType.Warning, coloring)
                    );
                }
            }

            if (helpBoxes.Count > 0)
            {
                EditorGUILayout.Space(style.PreHelpBoxAreaSpace);
                foreach ((string label, MessageType type, FieldColorData coloring) in helpBoxes)
                {
                    StartColoring(coloring.HelpBoxBackground, coloring.HelpBoxForeground);
                    EditorGUILayout.HelpBox(label, type);
                    EndColoring();
                    EditorGUILayout.Space(style.PostHelpBoxSpace);
                }
            }

            this.serializedObject.ApplyModifiedProperties();
        }

        private void LoadData()
        {
            this._inspectedType = serializedObject.targetObject.GetType();
            this._fields = this._inspectedType.GetFields(
                BindingFlags.Instance |
                BindingFlags.FlattenHierarchy |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.IgnoreCase
            );

            List<SerializedField> readOnly = new List<SerializedField>();
            List<SerializedField> rest = new List<SerializedField>();

            for (int i = 0; i < this._fields.Length; i++)
            {
                FieldInfo field = this._fields[i];
                bool isReadOnly = false;

                SerializedProperty property = serializedObject.FindProperty(field.Name);
                if (property != null)
                {
                    SerializedField serializedField = new SerializedField { Field = field, Property = property };
                    foreach (object attribute in field.GetCustomAttributes())
                    {
                        switch (attribute)
                        {
                            case HeaderAttribute headerAttribute:
                                serializedField.Header = headerAttribute.Header;
                                serializedField.HeaderTooltip = headerAttribute.Tooltip;
                                break;

                            case PropertyDrawerAttribute propertyDrawer:
                                serializedField.Action = propertyDrawer.Data;
                                break;

                            case ReadOnlyAttribute:
                                isReadOnly = true;
                                serializedField.IsReadOnly = true;
                                break;

                            case RequiredAttribute:
                                serializedField.IsRequired = true;
                                break;

                            case InspectorCovariantAttribute covariant:
                                serializedField.ConcreteTypes = covariant.Concrete;
                                serializedField.ConcreteTypenames = Array.ConvertAll(covariant.Concrete, t => t.FullName);
                                break;
                        }
                    }

                    if (isReadOnly)
                    {
                        readOnly.Add(serializedField);
                    }
                    else
                    {
                        rest.Add(serializedField);
                    }
                }
            }

            this._serializedFields = new List<SerializedField>(readOnly);
            this._serializedFields.AddRange(rest);
        }

        private void StartColoring(Color background, Color foreground)
        {
            this._previousBackground = GUI.backgroundColor;
            this._previousForeground = GUI.contentColor;

            GUI.backgroundColor = background;
            GUI.contentColor = foreground;
        }

        private void EndColoring()
        {
            GUI.backgroundColor = this._previousBackground;
            GUI.contentColor = this._previousForeground;
        }
    }
}