using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SkillProfi.WfpClient.Services.FormatConverter;

public sealed class BoolToBrushConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is bool booleanValue)
		{
			return booleanValue ? Brushes.Gray : Brushes.Red;
		}
		
		return Brushes.Gray;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value;
	}
}
