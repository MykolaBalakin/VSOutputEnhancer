using System;

namespace Balakin.VSOutputEnhancer.Parsers
{
    // TODO: Review accessibility level
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumValueAttribute : Attribute
    {
        public String Value { get; set; }

        public EnumValueAttribute(String value)
        {
            Value = value;
        }
    }
}