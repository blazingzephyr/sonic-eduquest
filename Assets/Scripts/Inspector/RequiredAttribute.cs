
using System;

namespace SonicEduquest
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RequiredAttribute : Attribute
    {
        public RequiredAttribute()
        {

        }
    }
}