using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestTask
{
    public class EnumToStringConverter<TEnum> : IValueConverter where TEnum : Enum
    {
        public EnumToStringConverter(params(TEnum value, string caption)[] captions)
        {
            captionsDict = captions.ToDictionary(kv => kv.value, kv => kv.caption);
        }
        private Dictionary<TEnum, string> captionsDict;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TEnum enumValue && captionsDict.TryGetValue(enumValue, out var caption))
                return caption;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
