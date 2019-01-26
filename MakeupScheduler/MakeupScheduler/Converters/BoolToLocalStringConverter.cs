using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MakeupScheduler.Converters
{
    public class BoolToLocalStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = (bool)value;
            return ToString(res);
        }

        private object ToString(bool res)
        {
            switch(res)
            {
                case true:
                    return "Da.";
                case false:
                default:
                    return "Ne";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
