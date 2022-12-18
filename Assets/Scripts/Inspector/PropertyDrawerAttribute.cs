
using System;

namespace SonicEduquest
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class PropertyDrawerAttribute : Attribute
    {
        public  Action<PropertyDrawerData>  Data    { get; }

        public PropertyDrawerAttribute(Action<PropertyDrawerData> data)
        {
            this.Data = data;
        }
    }
}