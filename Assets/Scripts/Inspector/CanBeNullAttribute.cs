
using System;

namespace SonicEduquest
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class CanBeNullAttribute : Attribute
    {
        public CanBeNullAttribute()
        {

        }
    }
}