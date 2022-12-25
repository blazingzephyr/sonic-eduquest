
using System;

namespace SonicEduquest
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class CanBeNullAttribute : Attribute
    {
        public  InspectorAttributeUsage Usage { get; }

        public CanBeNullAttribute(InspectorAttributeUsage usage = InspectorAttributeUsage.EditorAndPlaymode)
        {
            this.Usage = usage;
        }
    }
}