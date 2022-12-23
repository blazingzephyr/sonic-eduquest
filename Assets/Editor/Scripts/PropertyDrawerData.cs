
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SonicEduquest
{
    public readonly struct PropertyDrawerData
    {
        public  SerializedProperty  Property    { get; }
        public  FieldInfo           Field       { get; }
        public  Object              Target      { get; }

        public PropertyDrawerData(SerializedProperty property, FieldInfo field, Object target)
        {
            this.Property = property;
            this.Field = field;
            this.Target = target;
        }
    }
}