using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;

namespace BookReader.Converter
{
    public class NoRecordConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IList collection && collection.Count > 0)
            {
                return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}