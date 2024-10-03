using System.Globalization;
using System.Windows.Data;
using SkillProfi.WfpClient.Modules.ClientRequests.Models;

namespace SkillProfi.WfpClient.Modules.ClientRequests;

public sealed class StatusEnumConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is ClientRequestStatus status)
		{
			return StatusConverter.ToString(status);
		}

		return value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is string status)
		{
			return StatusConverter.ToEnum(status);
		}

		return value;
	}
}
