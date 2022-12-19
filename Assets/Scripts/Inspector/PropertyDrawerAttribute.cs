
using System;

namespace SonicEduquest
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class PropertyDrawerAttribute : Attribute
    {
        public int PropertyDrawerIndex { get; }

        public PropertyDrawerAttribute(int propertyDrawerIndex)
        {
            this.PropertyDrawerIndex = propertyDrawerIndex;
        }
    }
}