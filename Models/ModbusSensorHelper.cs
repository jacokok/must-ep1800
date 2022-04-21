using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Must.Models
{
    public static class ModbusSensorHelper
    {
        public static Dictionary<ushort, PropertyInfo> GetModbusSensorPropertyInfos<T>()
        {
            var dictionary = new Dictionary<ushort, PropertyInfo>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            var propertyInfos = properties
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ModbusSensorAttribute)));

            foreach (PropertyInfo property in propertyInfos)
            {
                if (property.GetCustomAttributes(true).First(y => y.GetType() == typeof(ModbusSensorAttribute)) is ModbusSensorAttribute attribute)
                {
                    dictionary.Add(attribute.Address, property);
                }
            }
            return dictionary;
        }
    }
}
