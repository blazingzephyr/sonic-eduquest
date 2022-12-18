
using System;

namespace SonicEduquest
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyAttribute : Attribute
    {
        public ReadOnlyAttribute()
        {

        }
    }
}