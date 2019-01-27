using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;

using Xamarin.Forms;

namespace MakeupScheduler.Converters
{
    public class ToLongDateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            if (dateTime.Equals(DateTime.MinValue))
                return "All";
            else
                return dateTime.ToShortDateString() + ConvertToLongString(dateTime);
        }

        private object ConvertToLongString(DateTime dateTime)
        {
            
            switch(dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return ", Ponedeljak";
                case DayOfWeek.Tuesday:
                    return ", Utorak";
                case DayOfWeek.Wednesday:
                    return ", Sreda";
                case DayOfWeek.Thursday:
                    return ", Cetvrtak";
                case DayOfWeek.Friday:
                    return ", Petak";
                case DayOfWeek.Saturday:
                    return ", Subota";
                default:
                    return ", Nedelja";     
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string date = (string)value;

            if (date == "All")
                date = DateTime.MinValue.ToShortTimeString();
            else
                date = date.Split(',').First();

            return DateTime.Parse(date);
        }
    }
}
