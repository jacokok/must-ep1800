using System;
using System.Collections.Generic;
using System.Text;

namespace Must.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SensorInterpretationAttribute : Attribute
    {
        public SensorInterpretationAttribute(string icon, string uom = "", bool publish = true)
        {
            Uom = uom;
            Icon = icon;
            Publish = publish;
        }

        public string Uom { get; }
        public string Icon { get; }
        public bool Publish { get; }
    }
}
