
using System;

namespace SonicEduquest
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RequiredAttribute : Attribute
    {
        public InspectorAttributeUsage Usage { get; }

        public RequiredAttribute(InspectorAttributeUsage usage = InspectorAttributeUsage.EditorAndPlaymode)
        {
            this.Usage = usage;
        }
    }
}