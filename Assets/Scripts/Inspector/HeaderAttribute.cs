
using System;

namespace SonicEduquest
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class HeaderAttribute : Attribute
    {
        public  PropertyVisibilityMode  VisibilityMode  { get; }
        public  string                  Header          { get; }
        public  string                  Tooltip         { get; }

        public HeaderAttribute(string header, string tooltip, PropertyVisibilityMode visibilityMode = PropertyVisibilityMode.EditorAndPlaymode)
        {
            this.VisibilityMode = visibilityMode;
            this.Header = header;
            this.Tooltip = tooltip;
        }
    }
}