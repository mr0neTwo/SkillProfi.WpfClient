using SkillProfi.WfpClient.Modules.ClientRequests.Models;

namespace SkillProfi.WfpClient.Modules.ClientRequests;

public static class StatusConverter
{
	public static ClientRequestStatus ToEnum(string stringStatus)
	{
		ClientRequestStatus enumStatus = stringStatus switch
		{
			"Получен" => ClientRequestStatus.Received,
			"В работе" => ClientRequestStatus.AtWork,
			"Выполнен" => ClientRequestStatus.Done,
			"Отклонен" => ClientRequestStatus.Denied,
			"Отменен" => ClientRequestStatus.Canceled,
			_ => ClientRequestStatus.Received
		};
		
		return enumStatus;
	}
	
	public static string ToString(ClientRequestStatus enumStatus)
	{
		string stringStatus = enumStatus switch
		{
			ClientRequestStatus.Received => "Получен",
			ClientRequestStatus.AtWork => "В работе",
			ClientRequestStatus.Done => "Выполнен",
			ClientRequestStatus.Denied => "Отклонен",
			ClientRequestStatus.Canceled => "Отменен",
			_ => string.Empty
		};
		
		return stringStatus;
	}
}
