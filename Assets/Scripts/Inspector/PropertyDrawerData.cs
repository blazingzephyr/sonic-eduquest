
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace SonicEduquest
{
    public readonly struct PropertyDrawerData
    {
        public  SerializedProperty  Property    { get; }
        public  VisualElement       Root        { get; }
        public  FieldInfo           Field       { get; }

        public PropertyDrawerData(SerializedProperty property, VisualElement root, FieldInfo field)
        {
            this.Property = property;
            this.Root = root;
            this.Field = field;
        }
    }
}