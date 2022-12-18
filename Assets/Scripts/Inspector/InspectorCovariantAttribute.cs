
using System;

namespace SonicEduquest
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InspectorCovariantAttribute : Attribute
    {
        public  Type[]  Concrete    { get; }

        public InspectorCovariantAttribute(params Type[] concreteTypes)
        {
            this.Concrete = concreteTypes;
        }
    }
}