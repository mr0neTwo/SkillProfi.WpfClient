namespace SkillProfi.WfpClient.Modules.ClientRequests;

public static class TimestampUtils
{
	public const long MaxValue = 253402300799;
	public const long MinValue = -62135596800;
	
	public static long GetEndOfDayTimestamp()
	{
		DateTime now = DateTime.Now;
		DateTime endOfDay = new(now.Year, now.Month, now.Day, 23, 59, 59, 999);

		return ConvertToUnixTimestamp(endOfDay);
	}

	public static long GetStartOfDayTimestamp()
	{
		DateTime now = DateTime.Now;
		DateTime startOfDay = new(now.Year, now.Month, now.Day, 0, 0, 0, 0);

		return ConvertToUnixTimestamp(startOfDay);
	}

	public static long GetEndOfWeekTimestamp()
	{
		DateTime now = DateTime.Now;
		int dayOfWeek = (int)now.DayOfWeek;
		int diff = dayOfWeek == 0 ? 0 : 7 - dayOfWeek;
		now = now.AddDays(diff);
		DateTime endOfWeek = new(now.Year, now.Month, now.Day, 23, 59, 59, 999);

		return ConvertToUnixTimestamp(endOfWeek);
	}

	public static long GetStartOfWeekTimestamp()
	{
		DateTime now = DateTime.Now;
		int dayOfWeek = (int)now.DayOfWeek;
		int diff = dayOfWeek == 0 ? -6 : 1 - dayOfWeek;
		now = now.AddDays(diff);
		DateTime startOfWeek = new(now.Year, now.Month, now.Day, 0, 0, 0, 0);

		return ConvertToUnixTimestamp(startOfWeek);
	}

	public static long GetStartOfMonthTimestamp()
	{
		DateTime now = DateTime.Now;
		DateTime startOfMonth = new(now.Year, now.Month, 1, 0, 0, 0, 0);

		return ConvertToUnixTimestamp(startOfMonth);
	}

	public static long GetEndOfMonthTimestamp()
	{
		DateTime now = DateTime.Now;
		DateTime firstDayOfNextMonth = new DateTime(now.Year, now.Month, 1).AddMonths(1);
		DateTime endOfMonth = firstDayOfNextMonth.AddDays(-1).Add(new TimeSpan(23, 59, 59, 999));

		return ConvertToUnixTimestamp(endOfMonth);
	}
	
	private static long ConvertToUnixTimestamp(DateTime dateTime)
	{
		return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
	}
}
