using System;

namespace Must.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SensorRemarksAttribute : Attribute
    {
        public SensorRemarksAttribute(string remarks)
        {
            Remarks = remarks;
        }

        public string Remarks { get; set; }
    }
}