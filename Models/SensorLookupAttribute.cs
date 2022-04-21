using System;

namespace Must.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SensorLookupAttribute : Attribute
    {
        public SensorLookupAttribute(string[] lookup)
        {
            Lookup = lookup;
        }

        public string[] Lookup { get; }
    }
}