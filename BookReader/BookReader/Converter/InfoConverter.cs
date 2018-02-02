using System;
using System.Globalization;
using Xamarin.Forms;

namespace BookReader.Converter
{
    public class InfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                value = "尚无" + parameter;
            }
            return string.Format("{0}:  {1}", parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}