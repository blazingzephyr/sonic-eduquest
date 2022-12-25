
using System;

namespace SonicEduquest
{
    public class PropertyVisibilityAttribute : Attribute
    {
        public  PropertyVisibilityMode  VisibilityMode { get; }

        public PropertyVisibilityAttribute(PropertyVisibilityMode visibilityMode)
        {
            this.VisibilityMode = visibilityMode;
        }
    }
}