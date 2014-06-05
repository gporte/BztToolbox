using System;
using System.Windows.Data;

namespace BztToolbox.Common.Utility
{
	[ValueConversion(typeof(bool), typeof(bool))]
	public class BooleanInversionConverter : IValueConverter
	{
		#region IValueConverter Membres

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (targetType != typeof(bool))
				throw new InvalidOperationException("The target must be a boolean");

			return !(bool)value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}

		#endregion
	}
}
