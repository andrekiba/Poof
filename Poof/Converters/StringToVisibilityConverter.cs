using System;
using Xamarin.Forms;

namespace Poof.Converters
{
	public class StringToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var stringValue = value as string;
			return !(string.IsNullOrEmpty(stringValue) && string.IsNullOrWhiteSpace(stringValue));
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}

	}
}
