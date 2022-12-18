
using System;

namespace SonicEduquest
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class HeaderAttribute : Attribute
    {
        public  string  Header  { get; }
        public  string  Tooltip { get; }

        public HeaderAttribute(string header, string tooltip)
        {
            this.Header = header;
            this.Tooltip = tooltip;
        }
    }
}