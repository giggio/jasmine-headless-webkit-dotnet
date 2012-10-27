using System;
using System.ComponentModel;

namespace jasmine_headless_webkit_dotnet
{
    public class ArrayOfStringConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof (string));
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var text = (string) value;
            var splited = text.Split(',');
            return splited;
        }
    }
}