using System.Globalization;
using System.Windows.Data;

namespace SkillProfi.WfpClient.Services.FormatConverter;

public sealed class TimestampToDateConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is long timestamp)
		{
			var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;

			return dateTime.ToString("yyyy-MM-dd HH:mm");
		}

		return value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is string dateString)
		{
			if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", culture, DateTimeStyles.None, out DateTime dateTime))
			{
				var timestamp = new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
				
				return timestamp;
			}
		}

		return value;
	}
}