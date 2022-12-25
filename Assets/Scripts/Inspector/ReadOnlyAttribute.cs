
using System;

namespace SonicEduquest
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyAttribute : Attribute
    {
        public  InspectorAttributeUsage Usage { get; }

        public ReadOnlyAttribute(InspectorAttributeUsage usage = InspectorAttributeUsage.EditorAndPlaymode)
        {
            this.Usage = usage;
        }
    }
}